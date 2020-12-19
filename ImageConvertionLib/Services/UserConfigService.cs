using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using ImageConverterLib.Helpers;
using ImageConverterLib.Library.DataFlow;
using ImageConverterLib.Models;
using ImageConverterLib.Repository;
using Serilog;

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
            return new UserConfigModel { ImageModels = new List<ImageModel>(), OutputDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "ConvertedImages") };
        }

        public bool AddImageToProcessQueue(string filePath, ref EventMessageQueue eventMessageQueue)
        {
            try
            {
                // Validate access and existence
                if (!File.Exists(filePath))
                {
                    eventMessageQueue.AddMessage("A file with specified path does not exist");
                    return false;
                }

                // Validate uniqueness
                if (_userConfig.ImageModels.Any(x => x.FilePath.Equals(filePath, StringComparison.CurrentCultureIgnoreCase)))
                {
                    eventMessageQueue.AddMessage("Tried to add a filePath which is already in the process queue.");
                    return false;
                }


                var model = ImageModel.CreateImageModel(filePath);
                var fi = new FileInfo(filePath);

                model.SortOrder = GetNextSortOrder();
                model.FileName = fi.Name;
                model.CreationTime = fi.CreationTime;
                model.Extension = fi.Extension;
                model.DirectoryName = fi.DirectoryName;
                model.FileSize = fi.Length;

                model.DisplayName = $"{model.FileName} - Size: {FileNameParser.GetFileSizeWithPrefix(model.FileSize)}";


                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "AddImageToProcessQueue threw an exception", nameof(filePath));
                eventMessageQueue.AddMessage("Argument exception was thrown because the file dows not exist");
            }

            return false;
        }

        private int GetNextSortOrder()
        {
            int maxSortOrder = _userConfig.ImageModels.Max(x => x.SortOrder);

            if (maxSortOrder != _userConfig.ImageModels.Count)
            {
                maxSortOrder = OptimizeSortOrderValuesAndReturnLast() + 1;
            }
            else
            {
                maxSortOrder++;
            }
            
            return maxSortOrder;
        }

        private int OptimizeSortOrderValuesAndReturnLast()
        {
            if (_userConfig.ImageModels.Count == 0)
            {
                return 0;
            }
            
            var sortedList = _userConfig.ImageModels.OrderBy(x => x.SortOrder).ToList();
            for (int i = 0; i < sortedList.Count; i++)
            {
                sortedList[i].SortOrder = i;
            }

            return _userConfig.ImageModels.Last().SortOrder;


        }
    }
}