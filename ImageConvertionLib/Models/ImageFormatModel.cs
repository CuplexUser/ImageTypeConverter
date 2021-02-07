using System;
using System.Drawing.Imaging;
using System.Reflection;

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
        public ImageFormatModel()
        {
            Guid = Guid.Empty;
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

            // Using reflection to copy the ImageFormatGuid From System.drawing.ImageFormat to local model.
            // The guid is used when reformatting of the image occurs to ensure a fail-safe match not only using string comparison between file type or extension.
            var members = typeof(ImageFormat).GetMembers(BindingFlags.Static | BindingFlags.Public);
            foreach (MemberInfo memberInfo in members)
            {
                if (memberInfo.Name.IndexOf(name, StringComparison.CurrentCultureIgnoreCase) >= 0)
                {
                    if (memberInfo.MemberType == MemberTypes.Method)
                    {
                        var methodBase = memberInfo.Module.ResolveMethod(memberInfo.MetadataToken, null, null);
                        object retObj = methodBase.Invoke(memberInfo, null);

                        if (retObj is ImageFormat obj)
                        {
                            Guid = obj.Guid;
                            break;
                        }

                    }
                }
            }

            if (Guid == Guid.Empty)
            {
                Guid = Guid.NewGuid();
            }

            PublicName = $"{Name} | ({Extension})";

        }


        public Guid Guid { get; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string PublicName { get; set; }

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