using ImageConverterLib.Models;

namespace ImageConverterLib.Library
{
    //public delegate Comparison<ImageModel> ImageModelComparison(ImageModel x, ImageModel y);
    public static class ImageComparison
    {
        public static int ImageModelComparison(ImageModel x, ImageModel y)
        {
            if (x.SortOrder > y.SortOrder)
                return 1;
            if (x.SortOrder < y.SortOrder)
                return -1;

            return 0;
        }
    }
}