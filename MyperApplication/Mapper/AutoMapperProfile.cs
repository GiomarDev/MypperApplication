using AutoMapper;
using MyperApplication.Models;
using MyperApplication.TransferObject.Model;

namespace MyperApplication.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Trabajadore, Trabajadores>()
            .ForMember(dest => dest.NombreDepartamento, opt => opt.MapFrom(src => src.IdDepartamentoNavigation.NombreDepartamento))
            .ForMember(dest => dest.NombreDistrito, opt => opt.MapFrom(src => src.IdDistritoNavigation.NombreDistrito))
            .ForMember(dest => dest.NombreProvincia, opt => opt.MapFrom(src => src.IdProvinciaNavigation.NombreProvincia));
        }
    }
}
