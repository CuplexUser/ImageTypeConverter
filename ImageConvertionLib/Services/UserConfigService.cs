using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using ImageConverterLib.Helpers;
using ImageConverterLib.Library;
using ImageConverterLib.Library.DataFlow;
using ImageConverterLib.Models;
using ImageConverterLib.Repository;
using Serilog;

namespace ImageConverterLib.Services
{
    /// <summary>
    ///  UserConfigService
    /// </summary>
    /// <seealso cref="ImageConverterLib.Services.ServiceBase" />
    public class UserConfigService : ServiceBase
    {
        /// <summary>
        /// The user configuration repository
        /// </summary>
        private readonly UserConfigRepository _userConfigRepository;
        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;
        /// <summary>
        /// The user configuration
        /// </summary>
        private UserConfigModel _userConfig;

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public UserConfigModel Config
        {
            get { return _userConfig; }
        }

        /// <summary>
        /// Gets the maximum sort order.
        /// </summary>
        /// <value>
        /// The maximum sort order.
        /// </value>
        public int MaxSortOrder => _userConfig.ImageModels?.Max(x => x.SortOrder) ?? 0;

        /// <summary>
        /// Gets the minimum sort order.
        /// </summary>
        /// <value>
        /// The minimum sort order.
        /// </value>
        public int MinSortOrder => _userConfig.ImageModels?.Min(x => x.SortOrder) ?? 0;

        /// <summary>
        /// Gets the length of the process queue.
        /// </summary>
        /// <value>
        /// The length of the process queue.
        /// </value>
        public int ProcessQueueLength => _userConfig.ImageModels.Count;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserConfigService"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="userConfigRepository">The user configuration repository.</param>
        public UserConfigService(IMapper mapper, UserConfigRepository userConfigRepository)
        {
            _mapper = mapper;
            _userConfigRepository = userConfigRepository;
            _userConfig = CreateDefaultConfig();
            CreateOutputDir(_userConfig.OutputDirectory);

        }

        /// <summary>
        /// Loads the configuration.
        /// </summary>
        /// <returns></returns>
        public bool LoadConfig()
        {
            return false;
        }

        /// <summary>
        /// Saves the configuration.
        /// </summary>
        /// <returns></returns>
        public bool SaveConfig()
        {
            return false;
        }

        /// <summary>
        /// Sets the output folder.
        /// </summary>
        /// <param name="selectedPath">The selected path.</param>
        public void SetOutputFolder(string selectedPath)
        {
            _userConfig.OutputDirectory = selectedPath;
        }

        /// <summary>
        /// Creates the output dir.
        /// </summary>
        /// <param name="path">The path.</param>
        private void CreateOutputDir(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// Creates the default configuration.
        /// </summary>
        /// <returns></returns>
        private UserConfigModel CreateDefaultConfig()
        {
            return new UserConfigModel(new List<ImageModel>(), Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "ConvertedImages"));
        }

        /// <summary>
        /// Adds the image to process queue.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="eventMessageQueue">The event message queue.</param>
        /// <returns></returns>
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

                RebuildSortIndex();

                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "AddImageToProcessQueue threw an exception", nameof(filePath));
                eventMessageQueue.AddMessage(ex.Message);
            }

            return false;
        }

        /// <summary>
        /// Removes the image from process queue.
        /// </summary>
        /// <param name="imageGuid">The image unique identifier.</param>
        /// <param name="messageQueue">The message queue.</param>
        /// <returns></returns>
        public bool RemoveImageFromProcessQueue(Guid imageGuid, ref EventMessageQueue messageQueue)
        {
            if (_userConfig.ImageModels.All(x => x.UniqueId != imageGuid))
            {
                messageQueue.AddMessage($"Could not remove item from list, list sync error?");
                return false;
            }

            var model = _userConfig.ImageModels.First(x => x.UniqueId == imageGuid);
            bool result = _userConfig.ImageModels.Remove(model);

            if (result)
            {
                RebuildSortIndex();
            }

            return result;
        }

        /// <summary>
        /// Clears the process queue.
        /// </summary>
        public void ClearProcessQueue()
        {
            _userConfig.ImageModels.Clear();
        }

        /// <summary>
        /// Gets the next sort order.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Optimizes the sort order values and return last.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Sorts the image models.
        /// </summary>
        private void SortImageModels()
        {
            _userConfig.ImageModels.Sort(ImageComparison.ImageModelComparison);
        }

        /// <summary>
        /// Changes the list position.
        /// </summary>
        /// <param name="imageGuilds">The image guilds.</param>
        /// <param name="decrementSortIndex">if set to <c>true</c> [decrement sort index].</param>
        /// <returns></returns>
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


        /// <summary>
        /// Rebuilds the index of the sort.
        /// </summary>
        private void RebuildSortIndex()
        {
            for (int i = 0; i < _userConfig.ImageModels.Count; i++)
            {
                _userConfig.ImageModels[i].SortOrder = i;
            }
        }

        /// <summary>
        /// Gets the image model by identifier.
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        /// <returns></returns>
        public ImageModel GetImageModelById(Guid guid)
        {
            return _userConfig.ImageModels.SingleOrDefault(x => x.UniqueId == guid);
        }


    }
}