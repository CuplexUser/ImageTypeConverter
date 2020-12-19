namespace ImageConverterLib.Models
{
    /// <summary>
    ///  ImageFormatModel
    /// </summary>
    public class ImageFormatModel
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="ImageFormatModel"/> class from being created.
        /// </summary>
        private ImageFormatModel()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageFormatModel"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="extension">The extension.</param>
        /// <param name="sortOrder">The sort order.</param>
        public ImageFormatModel(string name, string extension, int sortOrder)
        {
            Name = name;
            Extension = extension;
            SortOrder = sortOrder;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the extension.
        /// </summary>
        /// <value>
        /// The extension.
        /// </value>
        public string Extension { get; set; }

        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        /// <value>
        /// The sort order.
        /// </value>
        public int SortOrder { get; set; }
    }
}