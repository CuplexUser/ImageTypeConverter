using AutoMapper;
using ImageConverterLib.Models;

namespace ImageConverterLib.Library.AutomapperProfile
{
    public class ModelMapperProfile : Profile
    {
        public ModelMapperProfile()
        {
            ImageModel.CreateMapping(this);
        }
    }
}