using System;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using Serilog;

namespace SevenZip.Storage.Models
{
    [SecurityCritical]
    public sealed class StorageManagerSettings
    {
        public int NumberOfThreads
        {
            get => _numberOfThreads;
            set
            {
                _numberOfThreads = value;
                _useMultithreading = _numberOfThreads > 1;
            }

        }

        public bool UseMultithreading
        {
            get => _useMultithreading;
            set
            {
                switch (value)
                {
                    case true when _numberOfThreads > 1:
                        _useMultithreading = true;
                        break;
                    case false:
                        _useMultithreading = false;
                        _numberOfThreads = 1;
                        break;
                }
            }
        }

        public bool UseEncryption
        {
            get
            {
                return _useEncryption;
            }
            set
            {
                if (_password == null && value)
                {
                    Log.Warning("UseEncryption requires a set password");
                }

                _useEncryption = value;

            }
        }

        [SecuritySafeCritical]
        private byte[] _password;

        [SecuritySafeCritical]
        private const string pwdDefault = "a47f44a0ddfd2a588e00aaa65f61bad15cabb7f9e881b85254190daa3b116e8f";

        private int _numberOfThreads;
        private bool _useMultithreading;
        private bool _useEncryption;


        public StorageManagerSettings()
        {
            NumberOfThreads = Environment.ProcessorCount;
            UseMultithreading = true;
            InitDefault();
        }

        private void InitDefault()
        {
            _password = Encoding.UTF8.GetBytes(pwdDefault);
            UseEncryption = true;
            SetProtectedPassword();
        }

        public StorageManagerSettings(bool useMultithreading, int numberOfThreads, string password)
        {
            UseMultithreading = useMultithreading;
            NumberOfThreads = numberOfThreads;
            _password = Encoding.UTF8.GetBytes(password);
            UseEncryption = true;

            SetProtectedPassword();
        }

        private StorageManagerSettings(int numberOfThreads, string password)
        {
            NumberOfThreads = numberOfThreads;
            UseMultithreading = numberOfThreads > 1;
            UseEncryption = true;
            _password = Encoding.UTF8.GetBytes(password);
            SetProtectedPassword();
        }

        [SecuritySafeCritical]
        private void SetProtectedPassword()
        {
            ProtectedMemory.Protect(_password, MemoryProtectionScope.SameProcess);
        }

        [SecuritySafeCritical]
        public string GetPassword()
        {
            ProtectedMemory.Unprotect(_password, MemoryProtectionScope.SameProcess);
            string password = Encoding.Default.GetString(_password);
            ProtectedMemory.Protect(_password, MemoryProtectionScope.SameProcess);

            return password;
        }

        public static StorageManagerSettings CreateSettingsWithSecureString(int numberOfThreads, string password)
        {
            var settings = new StorageManagerSettings(numberOfThreads, password);
            return settings;
        }
    }
}