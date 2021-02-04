using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using AutoMapper;
using ImageConverterLib.DataModels;
using ImageConverterLib.Models;

namespace ImageConverterLib.Library.AutomapperProfiles
{
    public class AppConfigProfile : Profile
    {
        public AppConfigProfile()
        {

            CreateMap<ApplicationSettingsModel, ApplicationSettingsDataModel>()
                .ForMember(s => s.ImageFormatExtension, o => o.MapFrom(d => d.ImageFormatExtension))
                .ForMember(s => s.InputDirectory, o => o.MapFrom(d => d.InputDirectory))
                .ForMember(s => s.LastAppStartTime, o => o.MapFrom(d => d.LastAppStartTime))
                .ForMember(s => s.OutputDirectory, o => o.MapFrom(d => d.OutputDirectory))
                .ForMember(s => s.FormStateDataModels, o => o.MapFrom(d => d.FormStateModels.Values.ToList()))
                .ReverseMap()
                .ForMember(s => s.FormStateModels, o => o.MapFrom(d => d.FormStateDataModels.ToDictionary(x => x.FormName)));

            CreateMap<FormStateModel, FormStateDataModel>()
                .ForMember(s => s.FormName, o => o.MapFrom(d => d.FormName))
                .ForMember(s => s.FormPosition, o => o.MapFrom(d => d.FormPosition))
                .ForMember(s => s.FormSize, o => o.MapFrom(d => d.FormSize))
                .ForMember(s => s.WindowsState, o => o.MapFrom(d => d.WindowState))
                .ReverseMap()
                .ForMember(s => s.FormType, o => o.MapFrom(d => Type.GetType(d.FormName)));

            CreateMap<PointDataModel, Point>()
                .ForMember(s => s.X, o => o.MapFrom(d => d.X))
                .ForMember(s => s.Y, o => o.MapFrom(d => d.Y))
                .ReverseMap()
                .ForMember(s => s.X, o => o.MapFrom(d => d.X))
                .ForMember(s => s.Y, o => o.MapFrom(d => d.Y));

            CreateMap<VectorDataModel, Size>()
                .ForMember(s => s.Height, o => o.MapFrom(d => d.Height))
                .ForMember(s => s.Width, o => o.MapFrom(d => d.Width))
                .ReverseMap()
                .ForMember(s => s.Height, o => o.MapFrom(d => d.Height))
                .ForMember(s => s.Width, o => o.MapFrom(d => d.Width));
        }

    }
}