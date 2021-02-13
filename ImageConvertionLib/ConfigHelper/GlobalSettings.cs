using System;
using System.IO;
using System.Reflection;

namespace ImageConverterLib.ConfigHelper
{
    public class GlobalSettings
    {
        internal const string KeyName = "ImgType986ebd18-ba81-4779-b6a7-c0b5dd4a80ab"; 
        private const string UserDbFileName = "ComputedHashData.bin";
        private static string _logFileName;
        private static string _userDataPath;
        private static bool _isInitialized;
        private static GlobalSettings _instance;

        public Guid InstanceID { get; } = Guid.NewGuid();

        public static GlobalSettings Settings
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GlobalSettings();
                }

                return _instance;
            }
        }

        public void UnitTestInitialize(string testDataPath)
        {
            if (_isInitialized)
                return;

            _isInitialized = true;

            _logFileName = "UnitTest.log";
            _userDataPath = testDataPath;

            if (!Directory.Exists(_userDataPath))
                Directory.CreateDirectory(_userDataPath);
        }

        public void Initialize(string executableAssemblyName, bool useApplicationDataFolder)
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


        private string GetAssemblyPath(string fullAssemblyPath)
        {
            if (fullAssemblyPath != null)
            {
                int lastSlash = fullAssemblyPath.LastIndexOf('\\');
                if (lastSlash > 0)
                    return fullAssemblyPath.Substring(0, lastSlash + 1);
            }
            return null;
        }

        public string GetUserDataDirectoryPath()
        {
            if (!IsInitialized)
                return null;

            return _userDataPath;
        }

        public bool IsInitialized
        {
            get { return _isInitialized; }
        }
    }
}