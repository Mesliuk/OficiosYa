using AutoMapper;
using OficiosYa.Api.Models;
using OficiosYa.Application.DTOs;

namespace OficiosYa.Api.Mappings
{
    public class RegistrationProfile : Profile
    {
        public RegistrationProfile()
        {
            CreateMap<RegisterClienteRequest, RegistroClienteDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Correo))
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion));

            CreateMap<RegisterProfesionalRequest, RegistroProfesionalDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Correo))
                .ForMember(dest => dest.OficiosIds, opt => opt.MapFrom(src => new System.Collections.Generic.List<int> { src.OficioId }));
        }
    }
}
