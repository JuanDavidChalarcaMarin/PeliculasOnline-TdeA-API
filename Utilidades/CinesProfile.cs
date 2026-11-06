using AutoMapper;
using api.DTOs;
using api.Entidades;

namespace api.Utilidades
{
    public class CinesProfile : Profile
    {
        public CinesProfile()
        {
            CreateMap<CineCreacionDTO, Cine>();
            CreateMap<Cine, CineDTO>();
        }
    }
}
