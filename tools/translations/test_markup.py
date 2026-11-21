import unittest
from translate import Engine, MARKER, clean_prose, rebase_readme_links, release_body, release_notes_for_tag

class FakeEngine(Engine):
    def __init__(self):
        pass
    def text(self, text):
        return clean_prose(text.replace('Open', '`Ouvrir`'), 'fr')

class MarkupTests(unittest.TestCase):
    def test_generated_delimiters_cannot_create_code(self):
        source = 'Open `file.exe` and `config.json`.\n'
        result = FakeEngine().markdown(source)
        self.assertIn('’Ouvrir’ `file.exe`', result)
    def test_code_inside_link_labels_is_not_translated(self):
        source = '[Open `Open.exe`](https://example.com/Open.exe)\n'
        result = FakeEngine().markdown(source)
        self.assertIn('`Open.exe`', result)
        self.assertIn('](https://example.com/Open.exe)', result)
    def test_fenced_commands_are_unchanged(self):
        source = 'Open application\n```sh\nOpen `file.exe`\n```\n'
        self.assertIn('```sh\nOpen `file.exe`\n```', FakeEngine().markdown(source))

class DocumentationTests(unittest.TestCase):
    def test_release_notes_keep_only_the_matching_version(self):
        body='DeckPilot 2.4.6 current changes\n\nDeckPilot 2.4.4 older changes\n\nDeckPilot 2.4.3 older changes'
        self.assertEqual(release_notes_for_tag('v2.4.4',body),'DeckPilot 2.4.4 older changes')
        self.assertEqual(release_notes_for_tag('v2.4.6',body),'DeckPilot 2.4.6 current changes')

    def test_release_notes_keep_matching_duplicate_sections(self):
        body='DeckPilot 1.9.11 app changes\n\nDeckPilot 1.9.10 older changes\n\nDeckPilot 1.9.11 Atelier changes'
        self.assertEqual(release_notes_for_tag('v1.9.11',body),'DeckPilot 1.9.11 app changes\n\nDeckPilot 1.9.11 Atelier changes')

    def test_legacy_release_prelude_is_kept_without_old_sections(self):
        body='DeckPilot Atelier 1.0.7 recovery changes\n\nDeckPilot 1.9.7 older changes'
        self.assertEqual(release_notes_for_tag('v1.9.8',body),'DeckPilot Atelier 1.0.7 recovery changes')

    def test_release_body_embeds_short_translations_and_links_complete_long_ones(self):
        short=release_body('v2.1.7','English notes',{'fr':'Notes françaises'},1000)
        self.assertIn('<summary>fr</summary>',short)
        self.assertIn('Notes françaises',short)
        long=release_body('v2.1.7','English notes',{'fr':'F'*800,'de':'D'*800},1000)
        self.assertIn(MARKER,long)
        self.assertIn('docs/releases/v2.1.7/README.fr.md',long)
        self.assertIn('docs/releases/v2.1.7/README.de.md',long)
        self.assertNotIn('F'*800,long)

    def test_readme_links_are_rebased(self):
        self.assertEqual(rebase_readme_links('[Atelier](docs/FACTORY.md) [License](third-party/NAudio-LICENSE.txt) [Firmware](firmware/README.md) [Web](https://example.com) [Section](#features)', 'fr'), '[Atelier](../../docs/FACTORY.md) [License](../../third-party/NAudio-LICENSE.txt) [Firmware](FIRMWARE.fr.md) [Web](https://example.com) [Section](#features)')
    def test_device_names_and_ui_labels_are_preserved(self):
        class DestructiveEngine(FakeEngine):
            def text(self, text): return text.replace('CABLE', 'wrong').replace('Soundboard', 'wrong')
        result=DestructiveEngine().markdown('Select **CABLE Output** and Soundboard · micro virtuel.\n')
        self.assertIn('**CABLE Output**', result)
        self.assertIn('Soundboard · micro virtuel', result)

if __name__ == '__main__':
    unittest.main()
