using System.Collections.Generic;
using System.Linq;
using ImageConverterLib.Models;

namespace ImageConverterLib.Configuration
{
    /// <summary>
    /// ImageFormatCollection for the combobox selection
    /// </summary>
    public class ImageFormatCollection
    {
        /// <summary>
        /// The image types
        /// </summary>
        private readonly Dictionary<int, ImageFormatModel> _imageTypes;

        /// <summary>
        /// Gets the image types.
        /// </summary>
        /// <value>
        /// The image types.
        /// </value>
        public ICollection<ImageFormatModel> ImageTypes
        {
            get { return _imageTypes.Select(x => x.Value).ToList(); }
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="ImageFormatCollection"/> class from being created.
        /// </summary>
        private ImageFormatCollection()
        {
            _imageTypes = new Dictionary<int, ImageFormatModel>();
            InitializeTypeList();
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public static ImageFormatCollection Create()
        {
            return new ImageFormatCollection();
        }

        /// <summary>
        /// Initializes the type list.
        /// </summary>
        private void InitializeTypeList()
        {
            _imageTypes.Add(1, new ImageFormatModel("*.jpg", "Jpeg Image (*.jpg)", 1));
            _imageTypes.Add(2, new ImageFormatModel("*.png", "PNG image (*.png)", 2));
            _imageTypes.Add(3, new ImageFormatModel("*.bmp", "Bitmap Image (*.bmp)", 1));
        }

    }
}
