"""Translate complete Markdown documents with Argos models; retain executable syntax."""
import argparse, hashlib, json, os, re, shutil, tempfile, time, urllib.error, urllib.request, zipfile
from pathlib import Path

LANGUAGES = {'fr':'fr','de':'de','es':'es','it':'it','pt-BR':'pt','nl':'nl','pl':'pl','ru':'ru','ja':'ja','ko':'ko','zh-CN':'zh','ar':'ar'}
MARKER = '<!-- deckpilot-translations:start -->'

def request(url):
    headers={'User-Agent':'DeckPilot-documentation'}
    if url.startswith('https://api.github.com/'):
        headers['Authorization']='Bearer '+os.environ['GH_TOKEN']
    for attempt in range(5):
        try:
            with urllib.request.urlopen(urllib.request.Request(url,headers=headers),timeout=120) as response:
                return response.read()
        except (urllib.error.URLError, TimeoutError, ConnectionError) as error:
            if isinstance(error,urllib.error.HTTPError) and error.code not in (408,429,500,502,503,504):raise
            if attempt==4:raise
            time.sleep(min(20,2**attempt))

def releases():
    result=[]
    for page in range(1,101):
        batch=json.loads(request(f'https://api.github.com/repos/lolo34dr/deckpilot/releases?per_page=100&page={page}'))
        result.extend(r for r in batch if not r['draft'])
        if len(batch)<100:return result
    raise RuntimeError('Release pagination exceeded limit')

def slug(text):
    return re.sub(r'[^\w\- ]','',text.lower()).replace(' ','-')

def prepare(out):
    out.mkdir(parents=True,exist_ok=True)
    source=Path('README.md').read_bytes()
    (out/'README.md').write_bytes(source)
    firmware=Path('firmware/README.md').read_bytes()
    (out/'firmware.md').write_bytes(firmware)
    notes=[]
    for r in releases():
        tag=r['tag_name']
        if not re.fullmatch(r'[A-Za-z0-9._-]+',tag):raise ValueError('Unsafe release tag')
        body=(r['body'] or '').partition(MARKER)[0].rstrip()
        notes.append({'tag':tag,'body':body,'id':r['id']})
    (out/'releases.json').write_text(json.dumps(notes,ensure_ascii=False),encoding='utf-8')
    (out/'manifest.json').write_text(json.dumps({'readme_sha256':hashlib.sha256(source).hexdigest(),'firmware_sha256':hashlib.sha256(firmware).hexdigest()}))

# Markdown controls, exact code, URLs, link destinations and existing French UI labels.
PROTECTED=re.compile(r'(`+[^`\n]*`+|!?\[[^\]\n]*\]\([^\)\n]*\)|<[^>]+>|https?://[^\s<>]+|DeckPilot|\b\d+(?:[.,]\d+)*\b|\*\*|\*|\||&[a-zA-Z#0-9]+;)')
UI=['Lancer avec Windows','Firmware du macropad…','Votre macropad','Connexion et diagnostic','Options avancées · fichier et arguments','Tester l’action','Hors ligne','Multimédia','Rechercher une action ou une application','Préférences de l’application','Annuler / Rétablir','Gérer les profils','Microphone Windows','Changer de sortie audio','appui court / appui long','Réduire les animations','Changer de profil','Commandes Windows','Enregistrer un raccourci','Activité récente']


class BPEProcessor:
    """Argos also supplies BPE packages (including Polish), not just SentencePiece."""
    def __init__(self,path,code):
        from sacremoses import MosesTokenizer, MosesDetokenizer, MosesPunctNormalizer
        from subword_nmt.apply_bpe import BPE
        self.tokenizer=MosesTokenizer(lang='en')
        self.detokenizer=MosesDetokenizer(lang=code)
        self.normalizer=MosesPunctNormalizer(lang='en')
        with path.open(encoding='utf-8') as stream:self.bpe=BPE(stream)
    def encode(self,text,out_type=str):
        return self.bpe.segment_tokens(self.tokenizer.tokenize(self.normalizer.normalize(text)))
    def decode(self,tokens):
        return self.detokenizer.detokenize(' '.join(tokens).replace('@@ ','').split())

class Engine:
    def __init__(self,language,cache):
        import ctranslate2, sentencepiece
        code=LANGUAGES[language]
        index=json.loads(request('https://raw.githubusercontent.com/argosopentech/argospm-index/main/index.json'))
        package=next(p for p in index if p['from_code']=='en' and p['to_code']==code)
        directory=cache/code
        directory.mkdir(parents=True,exist_ok=True)
        model=next(directory.rglob('model.bin'),None)
        if model is None:
            archive=request(next(u for u in package['links'] if u.startswith('https://')))
            with tempfile.NamedTemporaryFile(suffix='.zip') as f:
                f.write(archive);f.flush()
                with zipfile.ZipFile(f.name) as z:
                    for item in z.infolist():
                        dest=(directory/item.filename).resolve()
                        if not dest.is_relative_to(directory.resolve()) or item.file_size>2_000_000_000:raise ValueError('Unsafe model archive')
                    z.extractall(directory)
            model=next(directory.rglob('model.bin'))
        sp_model=next(directory.rglob('sentencepiece.model'),None)
        bpe_model=next(directory.rglob('bpe.model'),None)
        if sp_model:self.processor=sentencepiece.SentencePieceProcessor(model_file=str(sp_model))
        elif bpe_model:self.processor=BPEProcessor(bpe_model,code)
        else:raise RuntimeError('No supported tokenizer found in '+str(directory))
        self.translator=ctranslate2.Translator(str(model.parent),device='cpu',compute_type='int8',inter_threads=1,intra_threads=4)
        self.cache={}

    def text(self,text):
        if not text.strip() or not re.search('[A-Za-z]',text):return text
        if text in self.cache:return self.cache[text]
        left=text[:len(text)-len(text.lstrip())];right=text[len(text.rstrip()):]
        value=text.strip()
        tokens=self.processor.encode(value,out_type=str)
        # Keep long prose below model context without dropping any sentence.
        if len(tokens)>350:
            chunks=re.split(r'(?<=[.!?;])\s+',value)
            if len(chunks)==1:
                words=value.split();chunks=[' '.join(words[i:i+100]) for i in range(0,len(words),100)]
            translated=' '.join(self.text(c) for c in chunks)
        else:
            result=self.translator.translate_batch([tokens],beam_size=4,max_decoding_length=768)[0]
            translated=self.processor.decode(result.hypotheses[0])
        if not translated.strip():raise RuntimeError('Translation model returned empty prose')
        answer=left+translated+right
        self.cache[text]=answer
        return answer

    def inline(self,line):
        # Translate link labels independently; never change their destination.
        result=[]
        for i,part in enumerate(PROTECTED.split(line)):
            if i%2:
                m=re.fullmatch(r'(!?)\[([^\]]*)\]\(([^)]*)\)',part)
                if m:part=f'{m[1]}[{self.text(m[2])}]({m[3]})'
                elif part.startswith('<'):
                    part=re.sub(r'(alt=")([^"]*)(")',lambda m:m[1]+self.text(m[2])+m[3],part)
                result.append(part)
            else:
                pattern='('+'|'.join(re.escape(x) for x in UI)+')'
                bits=re.split(pattern,part)
                result.append(''.join(b if j%2 else self.text(b) for j,b in enumerate(bits)))
        return ''.join(result)

    def markdown(self,source,readme=False):
        output=[];fence=False
        for line in source.splitlines():
            if line.startswith('```'):
                fence=not fence;output.append(line);continue
            if fence or not line.strip() or re.fullmatch(r'[ |:\-]+',line) or re.search('[\U0001F1E6-\U0001F1FF]',line) or line.strip().startswith(('<!--','</','<p ','<h1')):
                output.append(line);continue
            m=re.match(r'^(#{1,6} |\d+\. |- |> )?(.*)$',line)
            prefix=m[1] or '';body=m[2]
            if readme and prefix.startswith('#'):output.append(f'<a id="{slug(body)}"></a>')
            output.append(prefix+self.inline(body))
        if fence:raise ValueError('Unclosed Markdown fence')
        result='\n'.join(output)+'\n'
        if re.findall(r'```[\s\S]*?```',source)!=re.findall(r'```[\s\S]*?```',result):raise RuntimeError('Code block changed during translation')
        if re.findall(r'`[^`\n]+`',source)!=re.findall(r'`[^`\n]+`',result):raise RuntimeError('Inline code changed during translation')
        if len(re.findall(r'^#{1,6} ',source,re.M))!=len(re.findall(r'^#{1,6} ',result,re.M)):raise RuntimeError('Section omitted during translation')
        return result

def translate(language,source,out,cache):
    engine=Engine(language,cache)
    dest=out/'docs/i18n';dest.mkdir(parents=True,exist_ok=True)
    original=(source/'README.md').read_text()
    original=original.replace('src="deckpilot','src="../../deckpilot')
    original=original.replace('href="README.md"','href="../../README.md"')
    original=re.sub(r'href="docs/i18n/(README\.[^"]+)"',r'href="\1"',original)
    original=original.replace('](LICENSE)','](../../LICENSE)').replace('](firmware/README.md)',f'](FIRMWARE.{language}.md)')
    notice=engine.text('This is an automatic translation of the complete README. The English original is the reference. Application interface labels and commands are preserved exactly.')
    translated=engine.markdown(original,True)
    (dest/f'README.{language}.md').write_text('> '+notice+'\n\n'+translated,encoding='utf-8')
    if (source/'firmware.md').exists():
        firmware=(source/'firmware.md').read_text()
        firmware=re.sub(r'(\]\()([^):#]+)(\))',lambda m:m[1]+'../../firmware/'+m[2]+m[3],firmware)
        note=engine.text('This is an automatic translation of the complete firmware documentation. The English original is the reference. Commands and identifiers are preserved exactly.')
        (dest/f'FIRMWARE.{language}.md').write_text('> '+note+'\n\n'+engine.markdown(firmware,True),encoding='utf-8')
    release_notice=engine.text('This is an automatic translation of the complete release notes. The English original is the reference.')
    for release in json.loads((source/'releases.json').read_text()):
        directory=out/'docs/releases'/release['tag'];directory.mkdir(parents=True,exist_ok=True)
        body=release['body']
        (directory/f'README.{language}.md').write_text('> '+release_notice+'\n\n'+engine.markdown(body) if body else engine.text('No release notes were provided.')+'\n',encoding='utf-8')
    print(f'Completed {language}: full README and all published release notes',flush=True)

def publish(source):
    expected=json.loads((source/'manifest.json').read_text())['readme_sha256']
    if hashlib.sha256(Path('README.md').read_bytes()).hexdigest()!=expected:raise RuntimeError('README changed during translation; rerun against current source')
    manifest=json.loads((source/'manifest.json').read_text())
    if 'firmware_sha256' in manifest and hashlib.sha256(Path('firmware/README.md').read_bytes()).hexdigest()!=manifest['firmware_sha256']:raise RuntimeError('Firmware documentation changed during translation')
    for release in json.loads((source/'releases.json').read_text()):
        directory=Path('docs/releases')/release['tag'];directory.mkdir(parents=True,exist_ok=True)
        (directory/'README.en.md').write_text(release['body']+'\n',encoding='utf-8')
        contents=[release['body'],'',MARKER,'']
        for lang in LANGUAGES:
            body=(directory/f'README.{lang}.md').read_text()
            contents+=['<details>',f'<summary>{lang}</summary>','',body,'</details>','']
        body='\n'.join(contents)
        if len(body)>120000:raise RuntimeError('Translated release exceeds GitHub body size; no truncation permitted')
        current=json.loads(request(f"https://api.github.com/repos/lolo34dr/deckpilot/releases/{release['id']}"))
        if (current['body'] or '').partition(MARKER)[0].rstrip()!=release['body']:raise RuntimeError('Release notes edited during translation; refusing overwrite')
        if current['body']==body:continue
        payload=json.dumps({'body':body}).encode()
        headers={'Authorization':'Bearer '+os.environ['GH_TOKEN'],'User-Agent':'DeckPilot-documentation','Content-Type':'application/json'}
        req=urllib.request.Request(f"https://api.github.com/repos/lolo34dr/deckpilot/releases/{release['id']}",data=payload,headers=headers,method='PATCH')
        with urllib.request.urlopen(req,timeout=60) as response:response.read()

def main():
    p=argparse.ArgumentParser();p.add_argument('command',choices=['prepare','translate','all','publish']);p.add_argument('--language',choices=LANGUAGES);p.add_argument('--source',type=Path,default=Path('/tmp/deckpilot-source'));p.add_argument('--out',type=Path,default=Path('/tmp/deckpilot-translated'));p.add_argument('--cache',type=Path,default=Path('/tmp/deckpilot-models'));a=p.parse_args()
    if a.command=='prepare':prepare(a.source)
    elif a.command=='translate':translate(a.language,a.source,a.out,a.cache)
    elif a.command=='all':
        from concurrent.futures import ThreadPoolExecutor
        with ThreadPoolExecutor(max_workers=2) as pool:
            futures={lang:pool.submit(translate,lang,a.source,a.out,a.cache) for lang in LANGUAGES}
            for lang,future in futures.items():
                future.result()
                print('::notice title=Translation progress::'+lang+' completed',flush=True)
    else:publish(a.source)

if __name__ == '__main__':
    try:
        main()
    except Exception as error:
        message=(type(error).__name__+': '+str(error)).replace('%','%25').replace('\r','%0D').replace('\n','%0A')
        print('::error title=Documentation translation::'+message,flush=True)
        raise
