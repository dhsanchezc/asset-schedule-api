using AssetScheduleApi.Models.DTOs;
using AssetScheduleApi.Models.Entities;
using AutoMapper;

namespace AssetScheduleApi.Utils
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Model to DTO mappings
            //CreateMap<Asset, AssetOutput>();
            CreateMap<Asset, AssetOutput>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt));

            // DTO to Model mappings
            CreateMap<AssetInput, Asset>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
            // ... other mappings ...
        }
    }
}
