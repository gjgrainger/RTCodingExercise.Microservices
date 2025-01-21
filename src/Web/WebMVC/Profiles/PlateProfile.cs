using AutoMapper;
using Catalog.API.DTOs;
using WebMVC.Models;

namespace WebMVC.Profiles
{
    public class PlateProfile : Profile
    {
        public PlateProfile()
        {
            CreateMap<PlateDto, PlateViewModel>()
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
