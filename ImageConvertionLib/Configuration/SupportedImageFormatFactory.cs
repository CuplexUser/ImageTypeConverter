using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using ImageConverterLib.Models;

namespace ImageConverterLib.Configuration
{
    /// <summary>
    ///    SupportedImageFormatFactory
    /// </summary>
    public sealed class SupportedImageFormatFactory
    {
        /// <summary>
        /// The image format collection
        /// </summary>
        private readonly ImageFormatCollection _imageFormatCollection;

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <returns></returns>
        internal static SupportedImageFormatFactory CreateInstance()
        {
            var imgFormatFactory = new SupportedImageFormatFactory();
            imgFormatFactory.Initialize();
            return imgFormatFactory;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            _imageFormatCollection.Clear();
            int index = 0;
            foreach (string format in _imgFormats)
            {
                var model = new ImageFormatModel(format, $".{format.ToLower(CultureInfo.CurrentCulture)}", index++);
                _imageFormatCollection.Add(model);
            }
        }

        /// <summary>
        /// Gets the format models.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ImageFormatModel> GetFormatModels()
        {
            foreach (ImageFormatModel model in _imageFormatCollection)
            {
                yield return model;
            }
        }


        /// <summary>
        /// The img formats
        /// </summary>
        private string[] _imgFormats = new string[] { "Jpg", "Bmp", "Gif", "Png", "Tiff", "Webp" };

        /// <summary>
        /// Prevents a default instance of the <see cref="SupportedImageFormatFactory"/> class from being created.
        /// </summary>
        private SupportedImageFormatFactory()
        {
            _imageFormatCollection = new ImageFormatCollection();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="System.Collections.Generic.ICollection{ImageFormatModel}" />
        private class ImageFormatCollection : ICollection<ImageFormatModel>
        {
            /// <summary>
            /// The format models
            /// </summary>
            private readonly List<ImageFormatModel> _formatModels;

            /// <summary>
            /// Initializes a new instance of the <see cref="ImageFormatCollection"/> class.
            /// </summary>
            public ImageFormatCollection()
            {
                _formatModels = new List<ImageFormatModel>();
            }

            /// <summary>
            /// Returns an enumerator that iterates through the collection.
            /// </summary>
            /// <returns>
            /// An enumerator that can be used to iterate through the collection.
            /// </returns>
            public IEnumerator<ImageFormatModel> GetEnumerator()
            {
                return ((IEnumerable<ImageFormatModel>)_formatModels).GetEnumerator();
            }

            /// <summary>
            /// Returns an enumerator that iterates through a collection.
            /// </summary>
            /// <returns>
            /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
            /// </returns>
            IEnumerator IEnumerable.GetEnumerator()
            {
                return _formatModels.GetEnumerator();
            }

            /// <summary>
            /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.
            /// </summary>
            /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            public void Add(ImageFormatModel item)
            {
                _formatModels.Add(item);
            }

            /// <summary>
            /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1" />.
            /// </summary>
            public void Clear()
            {
                _formatModels.Clear();
            }

            /// <summary>
            /// Determines whether this instance contains the object.
            /// </summary>
            /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            /// <returns>
            ///   <see langword="true" /> if <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, <see langword="false" />.
            /// </returns>
            public bool Contains(ImageFormatModel item)
            {
                return _formatModels.Contains(item);
            }

            /// <summary>
            /// Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.
            /// </summary>
            /// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
            /// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
            public void CopyTo(ImageFormatModel[] array, int arrayIndex)
            {
                int index = 0;
                for (int i = arrayIndex; i < array.Length; i++)
                {
                    if (index >= _formatModels.Count)
                    {
                        break;
                    }
                    array[i] = _formatModels[index];
                    index++;
                }
            }

            /// <summary>
            /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.
            /// </summary>
            /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            /// <returns>
            ///   <see langword="true" /> if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, <see langword="false" />. This method also returns <see langword="false" /> if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.
            /// </returns>
            public bool Remove(ImageFormatModel item)
            {
                return _formatModels.Remove(item);
            }

            /// <summary>
            /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.
            /// </summary>
            public int Count => _formatModels.Count;

            /// <summary>
            /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.
            /// </summary>
            public bool IsReadOnly => false;
        }
    }
}