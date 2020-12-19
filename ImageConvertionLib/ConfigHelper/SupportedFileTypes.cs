using System.Collections.Generic;
using System.Linq;
using ImageConverterLib.Properties;

namespace ImageConverterLib.ConfigHelper
{
    public static class SupportedFileTypes
    {
        private static string[] _supportedInputFormats;
        private static string[] _supportedOutputFormats;

        public static IEnumerable<string> GetSupportedInputFormats()
        {
            if (_supportedInputFormats == null)
            {
                _supportedInputFormats = Settings.Default.SupportedInputFileTypes.Split(";".ToCharArray());
            }

            return _supportedInputFormats.ToList();
        }

        public static IEnumerable<string> GetSupportedOutputFormats()
        {
            if (_supportedOutputFormats == null)
            {
                _supportedOutputFormats = Settings.Default.SupportedOutputFormats.Split(";".ToCharArray());
            }

            return _supportedOutputFormats.ToList();
        }
    }
}