using AutoMapper;
using api.DTOs;
using api.Entidades;

namespace api.Utilidades
{
    public class ActoresProfile : Profile
    {
        public ActoresProfile()
        {
            CreateMap<ActorCreacionDTO, Actor>()
                .ForMember(actor => actor.Foto, opciones => opciones.Ignore());
        }
    }
}
