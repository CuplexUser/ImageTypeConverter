using System.Collections.Generic;
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
        /// Gets the display member.
        /// </summary>
        /// <value>
        /// The display member.
        /// </value>
        public string DisplayMember => nameof(ImageFormatModel.Name);

        /// <summary>
        /// Gets the value member.
        /// </summary>
        /// <value>
        /// The value member.
        /// </value>
        public string ValueMember =>  nameof(ImageFormatModel.Extension);

        /// <summary>
        /// Gets the image types.
        /// </summary>
        /// <value>
        /// The image types.
        /// </value>
        public IEnumerable<ImageFormatModel> GetImageTypes()
        {
            for (int i = 0; i < _imageTypes.Count; i++)
            {
                yield return _imageTypes[i];
            }
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
            _imageTypes.Add(0, new ImageFormatModel("*.jpg", "Jpeg Image (*.jpg)", 0));
            _imageTypes.Add(1, new ImageFormatModel("*.png", "PNG image (*.png)", 1));
            _imageTypes.Add(2, new ImageFormatModel("*.bmp", "Bitmap Image (*.bmp)", 2));
        }

    }
}
