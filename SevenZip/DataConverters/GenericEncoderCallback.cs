using System;
using System.Text;

namespace SevenZip.DataConverters
{
    public class GenericEncoderCallback
    {

        public static readonly Func<Encoding, string, byte[]> EncodeTextToBinary = delegate (Encoding encoder, string textToEncode)
        {
            byte[] result = encoder.GetBytes(textToEncode);
            return result;
        };

    }
}