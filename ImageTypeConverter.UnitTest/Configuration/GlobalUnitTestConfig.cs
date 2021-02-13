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

        internal static string TempDataPath
        {
            get
            {
                if (string.IsNullOrEmpty(_tempDataPath))
                {
                    _tempDataPath = Path.GetFullPath(Assembly.GetExecutingAssembly().Location + "..\\..\\..\\TempTestData");
                }

                return _tempDataPath;
            }
        }  
        
        internal static string UnitTestOutputPath
        {
            get
            {
                if (string.IsNullOrEmpty(_testRunnerOutputPath))
                {
                    _testRunnerOutputPath = Path.GetFullPath(Assembly.GetExecutingAssembly().Location + "..\\..\\..\\TestRunnerOutputPath");
                }

                return _tempDataPath;
            }
        }



        internal static void Initialize(TestContext context)
        {
            GlobalSettings.UnitTestInitialize(TempDataPath);
        }

    }
}