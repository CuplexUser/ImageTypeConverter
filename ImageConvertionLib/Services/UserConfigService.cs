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

        public int MaxSortOrder => _userConfig.ImageModels?.Max(x => x.SortOrder) ?? 0;

        public int MinSortOrder => _userConfig.ImageModels?.Min(x => x.SortOrder) ?? 0;

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
            return new UserConfigModel(new List<ImageModel>(), Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "ConvertedImages"));
        }

        public bool AddImageToProcessQueue(string filePath, ref EventMessageQueue eventMessageQueue)
        {
            try
            {
                // Validate access and existence
                if (!File.Exists(filePath))
                {
                    eventMessageQueue.AddMessage($"{filePath} - A file with specified path does not exist");
                    return false;
                }

                // Validate uniqueness
                if (_userConfig.ImageModels.Any(x => x.FilePath.Equals(filePath, StringComparison.CurrentCultureIgnoreCase)))
                {
                    eventMessageQueue.AddMessage($"{filePath} - Is already added to the process queue.");
                    return false;
                }


                var model = ImageModel.CreateImageModel(filePath);
                var fi = new FileInfo(filePath);

                model.SortOrder = GetNextSortOrder();
                model.FileName = fi.Name;
                model.CreationTime = fi.CreationTime;
                model.Extension = fi.Extension;
                model.DirectoryPath = fi.DirectoryName;
                model.FileSize = fi.Length;
                model.Size = FileNameParser.GetFileSizeWithPrefix(model.FileSize);

                model.DisplayName = $"FileName: {model.FileName}, Size{model.Size}";
                _userConfig.ImageModels.Add(model);

                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "AddImageToProcessQueue threw an exception", nameof(filePath));
                eventMessageQueue.AddMessage(ex.Message);
            }

            return false;
        }

        public bool RemoveImageFromProcessQueue(ImageModel imageModel, ref EventMessageQueue messageQueue)
        {
            if (_userConfig.ImageModels.All(x => x.UniqueId != imageModel.UniqueId))
            {
                messageQueue.AddMessage($"Could not remove ({imageModel.DisplayName}). Item does nor exist");
                return false;
            }

            ImageModel imageModelToRemove = _userConfig.ImageModels.Single(x => x.UniqueId == imageModel.UniqueId);
            bool result = _userConfig.ImageModels.Remove(imageModelToRemove);

            return result;
        }

        private int GetNextSortOrder()
        {
            if (_userConfig.ImageModels.Count == 0)
            {
                return 0;
            }

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

        private void SortImageModels()
        {
            _userConfig.ImageModels.Sort(SortOrderComparison);
        }

        private int SortOrderComparison(ImageModel x, ImageModel y)
        {
            if (x.SortOrder > y.SortOrder)
                return 1;
            if (x.SortOrder < y.SortOrder)
                return -1;

            return 0;
        }

        public bool ChangeListPosition(IEnumerable<Guid> imageGuilds, bool decrementSortIndex)
        {
            var models = _userConfig.ImageModels.Where(m => imageGuilds.Contains(m.UniqueId)).OrderBy(m => m.SortOrder).ToList();
            var nonSelectedModels = _userConfig.ImageModels.Where(m => !imageGuilds.Contains(m.UniqueId)).OrderBy(m => m.SortOrder).ToList();

            /*
             * List Shift algorithm
             *
             */

            //Move Up
            if (decrementSortIndex)
            {
                int min = models.Min(x => x.SortOrder);
                int max = models.Max(x => x.SortOrder);

                if (min > 0)
                {
                  
                    foreach (var model in models)
                    {
                        model.SortOrder--;
                    }

                    nonSelectedModels = nonSelectedModels.Where(m => models.Any(x => x.SortOrder == m.SortOrder)).ToList();

                    int index = models.Max(m => m.SortOrder) + 1;
                    for (int i = 0; i < nonSelectedModels.Count; i++)
                    {
                        nonSelectedModels[i].SortOrder = index + i;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                int max = models.Max(x => x.SortOrder);

                if (max < _userConfig.ImageModels.Count)
                {
                    foreach (var model in models)
                    {
                        model.SortOrder++;
                    }

                    nonSelectedModels = nonSelectedModels.Where(m => models.Any(x => x.SortOrder == m.SortOrder)).ToList();

                    int index = models.Min(m => m.SortOrder) - 1;
                    for (int i = 0; i < nonSelectedModels.Count; i++)
                    {
                        nonSelectedModels[i].SortOrder = index - i;
                    }

                }
                else
                {
                    return false;
                }

            }

            SortImageModels();
            RebuildSortIndex();
            return true;
        }


        private void RebuildSortIndex()
        {
            for (int i = 0; i < _userConfig.ImageModels.Count; i++)
            {
                _userConfig.ImageModels[i].SortOrder = i;
            }
        }

        public ImageModel GetImageModelById(Guid guid)
        {
            return _userConfig.ImageModels.SingleOrDefault(x => x.UniqueId == guid);
        }
    }
}