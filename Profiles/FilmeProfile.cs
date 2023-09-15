using AutoMapper;
using FilmesAPI_Alura.Data.DTOs;
using FilmesAPI_Alura.Models;

namespace FilmesAPI_Alura.Profiles
{
    public class FilmeProfile : Profile
    {
        public FilmeProfile()
        {
            CreateMap<CreateFilmeDTO, Filme>();
            CreateMap<UpdateFilmeDTO, Filme>();
            CreateMap<Filme, UpdateFilmeDTO>();

            CreateMap<Filme, ReadFilmeDTO>()
                .ForMember(filmeDTO => filmeDTO.Sessoes,
                opt => opt.MapFrom(filme => filme.Sessoes));
        }
    }
}
