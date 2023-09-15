using AutoMapper;
using FilmesAPI_Alura.Data.DTOs;
using FilmesAPI_Alura.Models;

namespace FilmesAPI_Alura.Profiles
{
    public class SessaoProfile : Profile
    {
        public SessaoProfile()
        {
            CreateMap<CreateSessaoDTO, Sessao>();
            CreateMap<Sessao, CreateSessaoDTO>();
            CreateMap<UpdateSessaoDTO, Sessao>();
        }
    }
}
