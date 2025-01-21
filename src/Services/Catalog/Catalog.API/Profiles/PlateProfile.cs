using AutoMapper;
using Catalog.API.DTOs;

namespace Catalog.API.Profiles
{
    public class PlateProfile : Profile
    {
        public PlateProfile()
        {
            CreateMap<Plate, PlateDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id)
                )
                .ForMember(
                    dest => dest.Registration,
                    opt => opt.MapFrom(src => src.Registration)
                )
                .ForMember(
                    dest => dest.Numbers,
                    opt => opt.MapFrom(src => src.Numbers)
                )
                .ForMember(
                    dest => dest.Letters,
                    opt => opt.MapFrom(src => src.Letters)
                )
                .ForMember(
                    dest => dest.PurchasePrice,
                    opt => opt.MapFrom(src => src.PurchasePrice)
                )
                .ForMember(
                    dest => dest.SalePrice,
                    opt => opt.MapFrom(src => src.SalePrice)
                )
                .ReverseMap();
        }
    }
}
