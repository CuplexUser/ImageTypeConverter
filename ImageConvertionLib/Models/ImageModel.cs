using System;
using AutoMapper;
using ImageConverterLib.DataModels;

namespace ImageConverterLib.Models
{
    /// <summary>
    ///     ImageModel
    /// </summary>
    public class ImageModel
    {
        /// <summary>
        ///     Gets or sets the name of the file.
        /// </summary>
        /// <value>
        ///     The name of the file.
        /// </value>
        public string FileName { get; set; }

        /// <summary>
        ///     Gets or sets the extension.
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
        ///     Gets or sets the created date.
        /// </summary>
        /// <value>
        ///     The created date.
        /// </value>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        ///     Gets or sets the full path.
        /// </summary>
        /// <value>
        ///     The full path.
        /// </value>
        public string FullPath { get; set; }

        public static void CreateMapping(IProfileExpression expression)
        {
            expression.CreateMap<ImageModel, ImageDataModel>()
                      .ForMember(s => s.FullPath, o => o.MapFrom(d => d.FullPath))
                      .ReverseMap();
        }
    }
}