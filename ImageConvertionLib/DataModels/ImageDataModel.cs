using System;
using System.IO;
using System.Runtime.Serialization;

namespace ImageConverterLib.DataModels
{
    /// <summary>
    ///  ImageDataModel 
    /// </summary>
    [DataContract(Name = "ImageDataModel")]
    public class ImageDataModel
    {
        /// <summary>
        /// Gets or sets the name of the file including file extension.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        [DataMember(Name = "FileName",Order = 1)]
        public string FileName{ get; set; }

        /// <summary>
        /// Gets or sets the extension.
        /// </summary>
        /// <value>
        /// The extension.
        /// </value>
        [DataMember(Name = "Extension", Order = 2)]
        public string Extension { get; set; }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        [DataMember(Name = "DirectoryPath", Order = 3)]
        public string DirectoryPath { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        [DataMember(Name = "DisplayName", Order = 4)]
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        /// <value>
        /// The sort order.
        /// </value>
        [DataMember(Name = "SortOrder", Order = 5)]
        public int SortOrder { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        [DataMember(Name = "CreatedDate", Order = 6)]
        public DateTime CreatedDate { get; set; }


        /// <summary>
        /// Gets the full path.
        /// </summary>
        /// <value>
        /// The full path.
        /// </value>
        public string FullPath => Path.Combine(DirectoryPath, FileName);
    }
}
