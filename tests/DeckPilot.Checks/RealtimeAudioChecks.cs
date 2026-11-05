using DeckPilot.Windows;
using NAudio.Wave;

internal static class RealtimeAudioChecks
{
    public static int Run()
    {
        int count=0;
        void Check(string name, Action test) { test(); count++; Console.WriteLine("PASS " + name); }
        void Expect(bool value) { if (!value) throw new Exception("Realtime audio regression"); }
        static byte[] Bytes(float[] values) { var bytes=new byte[values.Length*4]; Buffer.BlockCopy(values,0,bytes,0,bytes.Length); return bytes; }
        Check("Live microphone underruns pad only the requested block and do not delay the next captured block", () =>
        {
            var buffer=new RealtimeAudioBuffer(WaveFormat.CreateIeeeFloatWaveFormat(48000,1)); var output=new byte[480*4];
            Expect(buffer.Read(output,0,output.Length)==output.Length && output.All(b=>b==0));
            var input=Bytes(Enumerable.Repeat(.25f,480).ToArray()); buffer.AddSamples(input,0,input.Length);
            buffer.Read(output,0,output.Length); Expect(output.SequenceEqual(input) && buffer.BufferedBytes==0);
        });
        Check("A stalled consumer retains recent microphone frames instead of building seconds of latency", () =>
        {
            var buffer=new RealtimeAudioBuffer(WaveFormat.CreateIeeeFloatWaveFormat(1000,1),100);
            var first=Bytes(Enumerable.Range(0,80).Select(x=>(float)x).ToArray()); buffer.AddSamples(first,0,first.Length);
            var second=Bytes(Enumerable.Range(80,80).Select(x=>(float)x).ToArray()); buffer.AddSamples(second,0,second.Length);
            var output=new byte[400]; buffer.Read(output,0,400); Expect(BitConverter.ToSingle(output,0)==60 && BitConverter.ToSingle(output,396)==159);
            var large=Bytes(Enumerable.Range(0,250).Select(x=>(float)x).ToArray()); buffer.AddSamples(large,0,large.Length);
            buffer.Read(output,0,400); Expect(BitConverter.ToSingle(output,0)==150 && BitConverter.ToSingle(output,396)==249);
        });
        Check("Realtime resampling reads small blocks across microphone rates and channel layouts", () =>
        {
            foreach(int rate in new[]{8000,16000,44100,48000,96000})
            foreach(int channels in new[]{1,2})
            foreach(int outputChannels in new[]{1,2})
            {
                var input=new ConstantInput(rate,channels); var output=RealtimeAudio.Convert(input,WaveFormat.CreateIeeeFloatWaveFormat(48000,outputChannels));
                var block=new float[480*outputChannels];
                for(int i=0;i<30;i++) Expect(output.Read(block,0,block.Length)==block.Length);
                Expect(input.MaximumRead<=rate*channels*4/20);
                Expect(block.Skip(10*outputChannels).All(v=>Math.Abs(v-.25f)<.01f));
            }
        });
        Check("A live resampled microphone recovers on the next block after capture starvation", () =>
        {
            var input=new RealtimeAudioBuffer(WaveFormat.CreateIeeeFloatWaveFormat(44100,1));
            var output=RealtimeAudio.Convert(input,WaveFormat.CreateIeeeFloatWaveFormat(48000,2)); var block=new float[960];
            for(int i=0;i<5;i++)Expect(output.Read(block,0,block.Length)==block.Length);
            var voice=Bytes(Enumerable.Repeat(.25f,882).ToArray()); input.AddSamples(voice,0,voice.Length);
            output.Read(block,0,block.Length); Expect(block.Skip(100).All(x=>Math.Abs(x-.25f)<.01f));
        });
        Check("Windows extensible PCM and float microphone formats convert without native resamplers", () =>
        {
            foreach(int bits in new[]{16,32})
            foreach(int channels in new[]{1,2,6})
            {
                var format=new WaveFormatExtensible(48000,bits,channels); var input=new byte[480*channels*bits/8];
                for(int i=0;i<input.Length;i+=bits/8)
                    if(bits==16)BitConverter.TryWriteBytes(input.AsSpan(i,2),(short)8192);else BitConverter.TryWriteBytes(input.AsSpan(i,4),.25f);
                using var stream=new RawSourceWaveStream(new MemoryStream(input),format);
                var converted=RealtimeAudio.Convert(stream,WaveFormat.CreateIeeeFloatWaveFormat(48000,2)); var samples=new float[960];
                Expect(converted.Read(samples,0,samples.Length)==samples.Length && samples.All(x=>Math.Abs(x-.25f)<.001f));
            }
        });
        Check("Short sound clips drain and terminate through the same small-block converter", () =>
        {
            using var source=new RawSourceWaveStream(new MemoryStream(Bytes(Enumerable.Repeat(.25f,4410).ToArray())),WaveFormat.CreateIeeeFloatWaveFormat(44100,1));
            var output=RealtimeAudio.Convert(source,WaveFormat.CreateIeeeFloatWaveFormat(48000,2)); var block=new float[960];int total=0,read=0;
            for(int i=0;i<100;i++) { read=output.Read(block,0,block.Length);total+=read;if(read==0)break; }
            Expect(read==0 && Math.Abs(total-9600)<100);
        });
        Check("Capture and playback can access the bounded microphone buffer concurrently", () =>
        {
            var format=WaveFormat.CreateIeeeFloatWaveFormat(48000,2); var buffer=new RealtimeAudioBuffer(format); var input=new byte[3840]; var output=new byte[3840];
            Task.WaitAll(Task.Run(()=>{for(int i=0;i<1000;i++)buffer.AddSamples(input,0,input.Length);}),Task.Run(()=>{for(int i=0;i<1000;i++)buffer.Read(output,0,output.Length);}));
            Expect(buffer.BufferedBytes<=format.AverageBytesPerSecond/10 && buffer.BufferedBytes%format.BlockAlign==0);
        });
        return count;
    }
    private sealed class ConstantInput(int rate,int channels) : IWaveProvider
    {
        public WaveFormat WaveFormat {get;}=WaveFormat.CreateIeeeFloatWaveFormat(rate,channels);
        public int MaximumRead {get;private set;}
        public int Read(byte[] buffer,int offset,int count)
        { MaximumRead=Math.Max(MaximumRead,count);for(int i=0;i<count;i+=4)BitConverter.TryWriteBytes(buffer.AsSpan(offset+i,4),.25f);return count; }
    }
}
