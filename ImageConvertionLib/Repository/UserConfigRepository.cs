using System;
using System.IO;
using Autofac;
using AutoMapper;
using ImageConverterLib.DataModels;
using ImageConverterLib.Models;
using ImageConverterLib.Providers;

namespace ImageConverterLib.Repository
{
    public class UserConfigRepository: RepositoryBase
    {
        private readonly ILifetimeScope _scope;
        private readonly IMapper _mapper;
        private readonly FileSystemIOProvider _fileSystem;

        public UserConfigRepository(ILifetimeScope scope, IMapper mapper)
        {
            _scope = scope;
            _mapper = mapper;
            _fileSystem = new FileSystemIOProvider();
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

            UserConfigDataModel dataModel = _fileSystem.LoadUserConfig(filePath);
            var model = _mapper.Map<UserConfigModel>(dataModel);

            return model;
        }
    }
}