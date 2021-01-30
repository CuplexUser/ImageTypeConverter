using System;
using System.IO;
using System.IO.Compression;
using ImageConverterLib.DataModels;
using ProtoBuf;
using Serilog;

namespace ImageConverterLib.Providers
{
    public class FileSystemIOProvider : ProviderBase
    {
        public Guid InstanceId { get; }

        public FileSystemIOProvider()
        {
            InstanceId = Guid.NewGuid();
        }

        public ApplicationSettingsDataModel LoadApplicationSettings(string filename)
        {
            var model = LoadConfig<ApplicationSettingsDataModel>(filename);
            return model;
        }

        public bool SaveApplicationSettings(string filename, ApplicationSettingsDataModel appSettings)
        {
            return SaveConfig(filename, appSettings);
        }

        public UserConfigDataModel LoadUserConfig(string filename)
        {
            var model = LoadConfig<UserConfigDataModel>(filename);
            return model;
        }

        public bool SaveUserConfig(string filename, UserConfigDataModel userConfig)
        {
            return SaveConfig(filename, userConfig);
        }

        private T LoadConfig<T>(string filePath) where T : class
        {
            try
            {
                var fs = File.OpenRead(filePath);
                byte[] decompressedBytes = DeCompressDataStream(fs);
                var ms = new MemoryStream(decompressedBytes);

                return Serializer.Deserialize<T>(ms);

            }
            catch (Exception exception)
            {
                Log.Error(exception, "T LoadConfig<T> Exception");
                return default(T);
            }

        }

        private bool SaveConfig(string filePath, object model)
        {
            FileStream fs = null;
            try
            {
                fs = File.Create(filePath);
                var ms = new MemoryStream();
                Serializer.NonGeneric.Serialize(ms, model);

                byte[] compressedBytes = CompressDataStream(ms);

                fs.Write(compressedBytes, 0, compressedBytes.Length);
            }
            catch (Exception exception)
            {
                Log.Error(exception, "Exception thrown in the internal SaveConfig function.");
                return false;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Flush(true);
                    fs.Close();
                }
            }

            return true;
        }


        private byte[] CompressDataStream(MemoryStream inputStream)
        {
            var compressionStream = new MemoryStream();

            var deflateStream = new DeflateStream(compressionStream, CompressionLevel.Optimal);
            byte[] inputBytes = inputStream.ToArray();
            deflateStream.Write(inputBytes,0, inputBytes.Length);
            deflateStream.Close();
            deflateStream.Dispose();

            return compressionStream.ToArray();
        }

        private byte[] DeCompressDataStream(Stream fileStream)
        {
            var ms = new MemoryStream();
            var deflateStream = new DeflateStream(fileStream, CompressionMode.Decompress);
            byte[] buffer = new byte[64 * 1024];

            int bytesRead = 0;
            do
            {
                bytesRead = deflateStream.Read(buffer, 0, buffer.Length);
                ms.Write(buffer, 0, bytesRead);
                if (bytesRead < buffer.Length)
                {
                    break;
                }

            } while (bytesRead > 0);

            deflateStream.Close();
            deflateStream.Dispose();

            return ms.ToArray();
        }

        public override string ToString()
        {
            return $"File System IO Provider. Instance Id: {InstanceId}";
        }
    }
}