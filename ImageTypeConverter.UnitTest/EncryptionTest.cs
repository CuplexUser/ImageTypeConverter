using System;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SevenZip.DataConverters;
using SevenZip.Encryption;

namespace ImageTypeConverter.UnitTest
{
    [TestClass]
    public class EncryptionTest
    {
        private RandomGenerator _generator;

        [TestInitialize]
        public void InitTests()
        {
            _generator = new RandomGenerator();
        }

        
        [TestMethod]
        public void GenerateSalt()
        {
            byte[] buffer= new byte[32];
            _generator.GenerateRandomBytes(ref buffer);

            var sb = new StringBuilder();
            sb.AppendLine("32 byte of random data");

            sb.AppendLine(GeneralConverters.ByteArrayToHexString(buffer));

            sb.AppendLine("{");
            sb.Append("    ");

            foreach (byte b in buffer)
            {
                sb.Append("0x");
                sb.AppendFormat("{0:x2}", b);
                sb.Append(", ");
            }
            sb.Remove(sb.Length - 2, 2);

            sb.AppendLine();
            sb.AppendLine("};");

            int sum = 0;
            for (int i = 0; i < buffer.Length; i++)
            {
                sum += buffer[i];
            }
            Assert.IsTrue(sum > 0);

            Console.Write(sb.ToString());
        }
    }
}