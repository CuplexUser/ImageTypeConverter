using System;
using System.Globalization;
using System.IO;

namespace ImageConverterLib.Helpers
{
    public static class FileNameParser
    {
        private static readonly string[] BytePrefix = { "b", "Kb", "Mb", "Gb", "Tb", "Pb", "Eb", "Zb", "Yb" };

        public static string GetFileNameFromPath(string filePath)
        {
            return Path.GetFileName(filePath);
        }
        public static string GetDirectoryNameFromFullPath(string filePath)
        {
            return Path.GetDirectoryName(filePath);
        }

        public static string GetFileSizeWithPrefix(long length)
        {
            int index = 0;
            double result = Convert.ToDouble(length);
            while (length > 1024)
            {
                index++;
                length >>= 10;
            }

            result = Math.Ceiling((result + length) / (1024 ^ index));
            return result.ToString(CultureInfo.CurrentCulture.NumberFormat) + $" {BytePrefix[index]}";
        }
    }
}