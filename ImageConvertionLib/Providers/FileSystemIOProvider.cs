using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageConverterLib.DataModels;
using ProtoBuf;
using Serilog;
using SevenZip.Storage;
using SevenZip.Storage.Models;

namespace ImageConverterLib.Providers
{
    public class FileSystemIOProvider : ProviderBase
    {
        private readonly StorageManager _storageManager;

        public Guid InstanceId { get; }

        public FileSystemIOProvider()
        {
            InstanceId = Guid.NewGuid();
            var settings = new StorageManagerSettings();
            _storageManager = new StorageManager(settings);
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
                T config= _storageManager.DeserializeObjectFromFile<T>(filePath, null);

                return config;
                //var fs = File.OpenRead(filePath);
                //byte[] decompressedBytes = DeCompressDataStream(fs);
                //var ms = new MemoryStream(decompressedBytes);

                //return Serializer.Deserialize<T>(ms);

            }
            catch (Exception exception)
            {
                Log.Error(exception, "T LoadConfig<T> Exception");
                return default(T);
            }

        }

        private bool SaveConfig(string filePath, object model)
        {
            try
            {
                return _storageManager.SerializeObjectToFile(model, filePath, null);
            }
            catch (Exception exception)
            {
                Log.Error(exception, "Exception thrown in the internal SaveConfig function.");
                return false;
            }
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