using System;
using System.IO;
using SevenZip.Hashing;

namespace SevenZip.Storage.Models
{
    public class ArchiveFile
    {
        public const int BaseAllocSize = 0x18;
        public long StartPosition { get; set; }
        public long EndPosition { get; set; }
        public long FileInfoOffsetBytes { get; set; }
        public int UncompressedFileSize { get; set; }
        public int CompressedFileSize { get; set; }
        public ArchiveFileInfo FileInfo { get; private set; }

        private ArchiveFile()
        {
            FileInfo = new ArchiveFileInfo();
        }

        public static ArchiveFile CreateArchiveFile(FileInfo fileInfo)
        {
            ArchiveFile archiveFile = new ArchiveFile
            {
                FileInfo =
                {
                    Name = fileInfo.Name,
                    Attributes = (int) fileInfo.Attributes,
                    CreationTime = fileInfo.CreationTime,
                    Extension = fileInfo.Extension,
                    FileSize = fileInfo.Length,
                    FullName = fileInfo.FullName,
                    LastAccessTime = fileInfo.LastAccessTime,
                    LastWriteTime = fileInfo.LastWriteTime
                }
            };

            if (fileInfo.Directory != null)
                archiveFile.FileInfo.FullPath = fileInfo.Directory.FullName;

            CRC32 crc32hashTransform = new CRC32();
            archiveFile.FileInfo.CRC32 = crc32hashTransform.ComputeHash(fileInfo.OpenRead());

            return archiveFile;
        }


        public byte[] ToBytes()
        {
            MemoryStream memoryStream = new MemoryStream();
            byte[] fileInfoSerializedBytes = GetFileInfoToBytes();

            // Write start position
            byte[] bufferBytes = BitConverter.GetBytes(StartPosition);
            memoryStream.Write(bufferBytes, 0, bufferBytes.Length);

            // Write end position
            bufferBytes = BitConverter.GetBytes(EndPosition);
            memoryStream.Write(bufferBytes, 0, bufferBytes.Length);

            // Write decompressed block size
            bufferBytes = BitConverter.GetBytes(UncompressedFileSize);
            memoryStream.Write(bufferBytes, 0, bufferBytes.Length);

            // Write compressed block size
            bufferBytes = BitConverter.GetBytes(CompressedFileSize);
            memoryStream.Write(bufferBytes, 0, bufferBytes.Length);


            return memoryStream.ToArray();
        }

        private byte[] GetFileInfoToBytes()
        {
            MemoryStream ms = new MemoryStream();
            ProtoBuf.Serializer.NonGeneric.Serialize(ms, FileInfo);
            return ms.ToArray();
        }
    }
}