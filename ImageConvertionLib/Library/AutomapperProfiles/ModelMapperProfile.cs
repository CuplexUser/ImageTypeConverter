using AutoMapper;
using ImageConverterLib.DataModels;
using ImageConverterLib.ImageProcessing.Models;
using ImageConverterLib.Models;

namespace ImageConverterLib.Library.AutomapperProfiles
{
    public class ImageModelProfile : Profile
    {
        public ImageModelProfile()
        {
            CreateMap<ImageModel, ImageDataModel>()
                      .ForMember(s => s.FullPath, o => o.MapFrom(d => d.FilePath))
                      .ForMember(s => s.CreatedDate, o => o.MapFrom(d => d.CreationTime))
                      .ForMember(s => s.DirectoryPath, o => o.MapFrom(d => d.DirectoryName))
                      .ForMember(s => s.DisplayName, o => o.MapFrom(d => d.DisplayName))
                      .ForMember(s => s.Extension, o => o.MapFrom(d => d.Extension))
                      .ForMember(s => s.FileName, o => o.MapFrom(d => d.FileName))
                      .ForMember(s => s.SortOrder, o => o.MapFrom(d => d.SortOrder))
                      .ReverseMap();

            CreateMap<ImageModel, ImageProcessModel>()
                .ConstructUsing(x => new ImageProcessModel(x.UniqueId) {DirectoryPath = x.DirectoryName, Extension = x.Extension, FileName = x.FileName, FilePath = x.FilePath, FileSize = x.FileSize, SortOrder = x.SortOrder})
                .ReverseMap()
                .ConstructUsing(x => ImageModel.CreateImageModel(x.FilePath));

                      //.ForMember(s => s.SortOrder, o => o.MapFrom(d => d.SortOrder))
                      //.ForMember(s => s.Extension, o => o.MapFrom(d => d.Extension))
                      //.ForMember(s => s.FilePath, o => o.MapFrom(d => d.FilePath))
                      //.ForMember(s => s.FileName, o => o.MapFrom(d => d.FileName))
                      //.ForMember(s => s.FileSize, o => o.MapFrom(d => d.FileSize))
                      //.ReverseMap();
            
                      

            CreateMap<UserConfigModel, UserConfigDataModel>()
                .ForMember(s => s.ImageDataModels, o => o.MapFrom(d => d.ImageModels))
                .ForMember(s => s.OutputFileExtension, o => o.MapFrom(d => d.OutputFileExtension))
                .ForMember(s => s.OutputDirectory, o => o.MapFrom(d => d.OutputDirectory))
                .ReverseMap();
        }
    }
}