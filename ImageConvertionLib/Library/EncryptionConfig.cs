using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace ImageConverterLib.Library
{
    [SecurityCritical]
    internal sealed class EncryptionConfig
    {
        private readonly byte[] _keyBytes;

        [SecurityCritical]
        public EncryptionConfig()
        {
            UseDefaultKey = true;
            _keyBytes = null;
        }

        [SecurityCritical]
        public EncryptionConfig(string password)
        {
            _keyBytes = Encoding.UTF8.GetBytes(password);
            ProtectedMemory.Protect(_keyBytes, MemoryProtectionScope.SameProcess);
        }

        public bool UseDefaultKey { get; private set; }

        public byte[] GetKeyBytes()
        {
            return _keyBytes;
        }
    }
}