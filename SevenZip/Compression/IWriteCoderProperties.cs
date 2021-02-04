using System.IO;

namespace SevenZip.Compression.SevenZip
{
    public interface IWriteCoderProperties
    {
        void WriteCoderProperties(Stream outStream);
    }
}