namespace SevenZip.Compression.SevenZip.Compress.LZ
{
    internal interface IMatchFinder : IInWindowStream
    {
        void Create(uint historySize, uint keepAddBufferBefore, uint matchMaxLen, uint keepAddBufferAfter);
        uint GetMatches(uint[] distances);
        void Skip(uint num);
    }
}