using System;
using System.Runtime.Serialization;

namespace SevenZip.Storage.Models
{
    /// <summary>
    ///   ArchiveFileInfo
    /// </summary>
    [Serializable]
    [DataContract(Name = "ArchiveFileInfo")]
    public class ArchiveFileInfo
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [DataMember(Order = 0, Name = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        /// <value>
        /// The full name.
        /// </value>
        [DataMember(Order = 1, Name = "FullName")]
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the full path.
        /// </summary>
        /// <value>
        /// The full path.
        /// </value>
        [DataMember(Order = 2, Name = "FullPath")]
        public string FullPath { get; set; }

        /// <summary>
        /// Gets or sets the extension.
        /// </summary>
        /// <value>
        /// The extension.
        /// </value>
        [DataMember(Order = 3, Name = "Extension")]
        public string Extension { get; set; }

        /// <summary>
        /// Gets or sets the attributes.
        /// </summary>
        /// <value>
        /// The attributes.
        /// </value>
        [DataMember(Order = 4, Name = "Attributes")]
        public int Attributes { get; set; }

        /// <summary>
        /// Gets or sets the size of the file.
        /// </summary>
        /// <value>
        /// The size of the file.
        /// </value>
        [DataMember(Order = 5, Name = "FileSize")]
        public long FileSize { get; set; }

        /// <summary>
        /// Gets or sets the size of the compressed file.
        /// </summary>
        /// <value>
        /// The size of the compressed file.
        /// </value>
        [DataMember(Order = 6, Name = "CompressedFileSize")]
        public string CompressedFileSize { get; set; }

        /// <summary>
        /// Gets or sets the cr C32.
        /// </summary>
        /// <value>
        /// The cr C32.
        /// </value>
        [DataMember(Order = 7, Name = "CRC32")]
        public byte[] CRC32 { get; set; }

        /// <summary>
        /// Gets or sets the creation time.
        /// </summary>
        /// <value>
        /// The creation time.
        /// </value>
        [DataMember(Order = 8, Name = "CreationTime")]
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// Gets or sets the last write time.
        /// </summary>
        /// <value>
        /// The last write time.
        /// </value>
        [DataMember(Order = 9, Name = "LastWriteTime")]
        public DateTime LastWriteTime { get; set; }

        /// <summary>
        /// Gets or sets the last access time.
        /// </summary>
        /// <value>
        /// The last access time.
        /// </value>
        [DataMember(Order = 10, Name = "LastAccessTime")]
        public DateTime LastAccessTime { get; set; }
    }
}