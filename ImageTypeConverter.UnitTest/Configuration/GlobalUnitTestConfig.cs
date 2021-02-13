using System.IO;
using System.Reflection;
using ImageConverterLib.ConfigHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImageTypeConverter.UnitTest.Configuration
{
    internal static class GlobalUnitTestConfig
    {
        private static string _tempDataPath = null;
        private static string _testRunnerOutputPath = null;
        private const string _appSettingsFilename = "ImageConverterSettings.bin";

        internal static string TempDataPath
        {
            get
            {
                if (string.IsNullOrEmpty(_tempDataPath))
                {
                    _tempDataPath = Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..\\..\\..\\TempTestData"));
                }

                return _tempDataPath;
            }
        }  
        
        internal static string TestDataInputPath
        {
            get
            {
                if (string.IsNullOrEmpty(_testRunnerOutputPath))
                {
                    _testRunnerOutputPath = Path.GetFullPath(Assembly.GetExecutingAssembly().Location + "..\\..\\..\\TestDataInput");
                }

                return _tempDataPath;
            }
        }

        public static string AppSettingsFilename => _appSettingsFilename;


        internal static void Initialize(TestContext context)
        {
            GlobalSettings.Settings.UnitTestInitialize(TempDataPath);
        }

    }
}