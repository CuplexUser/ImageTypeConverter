using System;
using System.Collections.Generic;
using System.IO;
using AutoMapper;
using ImageConverterLib.Models;
using ImageConverterLib.Repository;

namespace ImageConverterLib.Services
{
    public class UserConfigService : ServiceBase
    {
        private readonly UserConfigRepository _userConfigRepository;
        private readonly IMapper _mapper;
        private UserConfigModel _userConfig;

        public UserConfigModel Config
        {
            get { return _userConfig; }
        }

        public UserConfigService(IMapper mapper, UserConfigRepository userConfigRepository)
        {
            _mapper = mapper;
            _userConfigRepository = userConfigRepository;
            _userConfig = CreateDefaultConfig();
            CreateOutputDir(_userConfig.OutputDirectory);

        }

        public bool LoadConfig()
        {
            return false;
        }

        public bool SaveConfig()
        {
            return false;
        }

        public void SetOutputFolder(string selectedPath)
        {
            _userConfig.OutputDirectory = selectedPath;
        }

        private void CreateOutputDir(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private UserConfigModel CreateDefaultConfig()
        {
            return new UserConfigModel {ImageModels = new List<ImageModel>(), OutputDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "ConvertedImages")};
        }
    }
}