using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SevenZip.Storage.Models
{
    public class CompressionArchive
    {
        private static readonly byte[] HeaderIdentifierBytes =
        {
            0x1C, 0xE4, 0x9E, 0xA8, 0xAA, 0x7D, 0x3E, 0xDD, 0x1C, 0x8B, 0x67, 0xCC, 0xC9, 0x7A, 0x05, 0xD6, 0x79, 0xD4, 0x9F, 0x6A, 0x7B, 0xCB, 0xE3, 0xA1, 0x5C, 0x7E, 0xBD, 0xFC, 0xC7, 0xA8, 0xDA, 0x69
        };

        private static readonly byte[] HeaderEndBytes =
        {
            0xC0, 0xA0, 0x54, 0xDA, 0x93, 0xE9, 0x4E, 0x63, 0x81, 0xF9, 0x0E, 0x47, 0x8C, 0x82, 0x7F, 0xB3, 0xC5, 0x97, 0x5D, 0x8F, 0xF2, 0xBE, 0x39, 0xA8, 0xA7, 0x6E, 0x76, 0xD6, 0x09, 0x4D, 0x9A, 0xDF
        };

        private const int HeaderPropertiesAllocSize = 0x14;

        public List<ArchiveFile> ArchiveFiles { get; private set; }

        public CompressionArchive()
        {
            ArchiveFiles = new List<ArchiveFile>();
        }

        public static bool VerifyFileHeader(Stream inputStream)
        {
            if (inputStream.Length < HeaderIdentifierBytes.LongLength)
                return false;

            byte[] inputHeaderBytes = new byte[HeaderIdentifierBytes.Length];
            inputStream.Read(inputHeaderBytes, 0, inputHeaderBytes.Length);
            inputStream.Position = 0;

            return HeaderIdentifierBytes.SequenceEqual(inputHeaderBytes);
        }

        public static CompressionArchive DecodeHeader(Stream inputStream)
        {
            CompressionArchive compressionArchive = new CompressionArchive();

            inputStream.Position = 0;
            if (!VerifyFileHeader(inputStream))
                throw new Exception("Incorrect file header");

            inputStream.Position = HeaderIdentifierBytes.Length;
            byte[] buffer = new byte[8];

            // Read file header size
            inputStream.Read(buffer, 0, 4);
            int fileHeaderSize = BitConverter.ToInt32(buffer, 0);

            return compressionArchive;
        }
    }
}