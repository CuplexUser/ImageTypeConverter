using System;
using System.IO;
using System.IO.Compression;
using ImageConverterLib.DataModels;
using ProtoBuf;
using Serilog;

namespace ImageConverterLib.Providers
{
    public class UserConfigDataIOProvider   : ProviderBase
    {
  

        public UserConfigDataIOProvider()
        {


        }

        public void SaveUserConfig(string filePath, UserConfigDataModel model)
        {
            FileStream fs = null;
            try
            {
                fs = File.Create(filePath);
                var ms = new MemoryStream();
                Serializer.NonGeneric.Serialize(ms, model);

                byte[] compressedBytes = CompressDataStream(ms);


            }
            catch (Exception exception)
            {
                Log.Error(exception, "Exception thrown in the internal SaveConfig function.");
            }
            finally
            {
                if (fs != null)
                {
                    fs.Flush(true);
                    fs.Close();
                }
            }

        }


        private byte[] CompressDataStream(MemoryStream inputStream)
        {
            var compressionStream = new MemoryStream();
            byte[] buffer = inputStream.ToArray();

            var deflateStream = new DeflateStream(compressionStream, CompressionLevel.Optimal);
            deflateStream.Write(buffer, 0, buffer.Length);
            deflateStream.Flush();
            compressionStream.Flush();

            return compressionStream.ToArray();
        }
      
    }
}