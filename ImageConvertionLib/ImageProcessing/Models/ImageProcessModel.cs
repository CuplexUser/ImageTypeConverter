namespace ImageConverterLib.ImageProcessing.Models
{
    public class ImageProcessModel
    {
        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        /// <value>
        /// The file path.
        /// </value>
        public string FilePath { get; set; }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; set; }

        /// <summary>
        ///     Gets or sets the File extension.
        /// </summary>
        /// <value>
        ///     The extension.
        /// </value>
        public string Extension { get; set; }

        /// <summary>
        ///     Gets or sets the directory path.
        /// </summary>
        /// <value>
        ///     The directory path.
        /// </value>
        public string DirectoryPath { get; set; }

        /// <summary>
        ///     Gets or sets the sort order.
        /// </summary>
        /// <value>
        ///     The sort order.
        /// </value>
        public int SortOrder { get; set; }

        /// <summary>
        /// Gets or sets the size of the file.
        /// </summary>
        /// <value>
        /// The size of the file.
        /// </value>
        public long FileSize { get; set; }
    }
}