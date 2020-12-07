using System;
using System.IO;
using Autofac;
using AutoMapper;
using ImageConverterLib.DataModels;
using ImageConverterLib.Models;
using ImageConverterLib.Providers;
using Serilog;

namespace ImageConverterLib.Repository
{
    public class UserConfigRepository: RepositoryBase
    {
        private readonly ILifetimeScope _scope;
        private readonly IMapper _mapper;
        private readonly UserConfigFileManager _configFileManager;

        public UserConfigRepository(ILifetimeScope scope, IMapper mapper)
        {
            _scope = scope;
            _mapper = mapper;
            var configDataIoProvider = new UserConfigDataIOProvider();
            _configFileManager = new UserConfigFileManager(configDataIoProvider);
        }

        public void SaveConfiguration(UserConfigModel userConfig)
        {

        }

        public UserConfigModel LoadConfiguration(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new ArgumentException("Failed to load Configuration\n"+nameof(filePath)+" - Does not exist");
            }

            var model = _mapper.Map<UserConfigDataModel, UserConfigModel>(_configFileManager.LoadModel(filePath));

            return model;
        }


        public class UserConfigFileManager
        {
            private readonly UserConfigDataIOProvider _userConfigDataIoProvider;
            public UserConfigFileManager(UserConfigDataIOProvider userConfigDataIoProvider)
            {
                _userConfigDataIoProvider = userConfigDataIoProvider;
            }

            public UserConfigDataModel LoadModel(string filePath)
            {
                UserConfigDataModel model = null;
                try
                {

                }
                catch (Exception exception)
                {
                    Log.Error(exception,"Failed to load the specified file");
                }

                return model;
            }
        }
    }
}