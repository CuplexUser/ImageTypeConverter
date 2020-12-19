using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using ImageConverterLib.Models;

namespace ImageConverterLib.Helpers
{
    public static class FileNameParser
    {
        private static readonly string[] BytePrefix = { "b", "Kb", "Mb", "GB", "TB" };
        //public static ImageModel UpdateImageModel(ref ImageModel model, string filePath)
        //{
        //    Regex fileNameRegex = new Regex(@"([\w]{1,200}\.[\w]{2,5}$)", RegexOptions.Singleline);
        //    FileInfo fileInfo = new FileInfo(filePath);
        //    model.FullPath = fileInfo.FullName;
        //    model.CreationTime = fileInfo.CreationTime;
            
        //    model.DirectoryName = fileInfo.DirectoryName;
        //    model.Extension = fileInfo.Extension;
        //    model.FileName = fileInfo.Name;
        //    model.DisplayName = $"{fileInfo.Name} - ({GetFileSizeWithPrefix(fileInfo.Length)})";

        //    return model;
        //}

        public static string GetFileSizeWithPrefix(long length)
        {
            string retVal = "";

            int sizeIndex = 0;
            double result = length;

            while (result > 1024)
            {
                sizeIndex++;
                result = result / 1024;
            }

            if (sizeIndex < 5)
            {
                result = Math.Round(result, 2);
                retVal = result.ToString(CultureInfo.CurrentCulture.NumberFormat) + $" {BytePrefix[sizeIndex]}";
            }
            else
            {
                retVal = "N/A";
            }


            return retVal;
        }
    }
}