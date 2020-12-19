using System;
using System.IO;
using AutoMapper;
using ImageConverterLib.DataModels;

namespace ImageConverterLib.Models
{
    /// <summary>
    ///     ImageModel
    /// </summary>
    public class ImageModel
    {
        protected ImageModel(string filePath)
        {
            UniqueId = Guid.NewGuid();
            FilePath = filePath;
        }

        public static ImageModel CreateImageModel(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new ArgumentException("File does not exist or is inaccessable.", nameof(filePath));
            }

            var imageModel = new ImageModel(filePath);
            return imageModel;
        }

        /// <summary>
        /// Gets the unique identifier.
        /// </summary>
        /// <value>
        /// The unique identifier.
        /// </value>
        public Guid UniqueId { get; }

        /// <summary>
        ///     returns the complete path to the image.
        /// </summary>
        /// <value>
        ///     The full path.
        /// </value>
        public string FilePath { get; }

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
        public string DirectoryName { get; set; }

        /// <summary>
        ///     Gets or sets the display name.
        /// </summary>
        /// <value>
        ///     The display name.
        /// </value>
        public string DisplayName { get; set; }

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

        /// <summary>
        ///     Gets or sets the created date.
        /// </summary>
        /// <value>
        ///     The created date.
        /// </value>
        public DateTime CreationTime { get; set; }


        public static void CreateMapping(IProfileExpression expression)
        {
            expression.CreateMap<ImageModel, ImageDataModel>()
                      .ForMember(s => s.FullPath, o => o.MapFrom(d => d.FilePath))
                      .ReverseMap();
        }
    }
}