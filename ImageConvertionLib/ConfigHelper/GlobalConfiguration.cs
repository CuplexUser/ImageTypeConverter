using System;
using System.IO;
using System.Reflection;

namespace ImageConverterLib.ConfigHelper
{
    public static class GlobalSettings
    {
        internal const string KeyName = "ImgType986ebd18-ba81-4779-b6a7-c0b5dd4a80ab"; 
        private const string UserDbFileName = "ComputedHashData.bin";
        private static string _logFileName;
        private static string _userDataPath;
        private static bool _isInitialized;

        public static void UnitTestInitialize(string testDataPath)
        {
            if (_isInitialized)
                return;

            _isInitialized = true;

            _logFileName = "UnitTest.log";
            _userDataPath = testDataPath;

            if (!Directory.Exists(_userDataPath))
                Directory.CreateDirectory(_userDataPath);
        }

        public static void Initialize(string executableAssemblyName, bool useApplicationDataFolder)
        {
            if (_isInitialized)
                return;

            _isInitialized = true;

            _logFileName = executableAssemblyName + ".log";
            if (useApplicationDataFolder)
                _userDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + executableAssemblyName + "\\";
            else
                _userDataPath = GetAssemblyPath(Assembly.GetExecutingAssembly().Location);

            if (!Directory.Exists(_userDataPath))
                Directory.CreateDirectory(_userDataPath);
        }


        private static string GetAssemblyPath(string fullAssemblyPath)
        {
            if (fullAssemblyPath != null)
            {
                int lastSlash = fullAssemblyPath.LastIndexOf('\\');
                if (lastSlash > 0)
                    return fullAssemblyPath.Substring(0, lastSlash + 1);
            }
            return null;
        }

        public static string GetUserDataDirectoryPath()
        {
            if (!Initialized)
                return null;

            return _userDataPath;
        }

        public static bool Initialized { get; }
    }
}