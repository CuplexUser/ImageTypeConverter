using System;
using System.Security.Cryptography;

namespace SevenZip.Encryption
{
    public class RandomGenerator  : IDisposable
    {
        private readonly RandomNumberGenerator _randomProvider;
        public RandomGenerator()
        {
            _randomProvider = RandomNumberGenerator.Create("System.Security.Cryptography.RandomNumberGenerator");
        }

        public void GenerateRandomBytes(ref byte[] bufferBytes)
        {
            _randomProvider.GetNonZeroBytes(bufferBytes);
        }

        public void Dispose()
        {
            _randomProvider?.Dispose();
        }
    }
}