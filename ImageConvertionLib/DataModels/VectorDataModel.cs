﻿using System.Runtime.Serialization;

namespace ImageConverterLib.DataModels
{
    /// <summary>
    ///   VectorDataModel
    /// </summary>
    /// <seealso cref="ImageConverterLib.DataModels.PointDataModel" />
    [DataContract(Name = "VectorDataModel")]
    public class VectorDataModel : PointDataModel
    {
        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        [DataMember(Name = "Width", Order = 1)]
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        [DataMember(Name = "Height", Order = 2)]
        public int Height { get; set; }
    }
}