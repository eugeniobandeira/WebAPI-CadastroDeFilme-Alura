using AutoMapper;
using FilmesAPI_Alura.Data.DTOs;
using FilmesAPI_Alura.Models;

namespace FilmesAPI_Alura.Profiles
{
    public class CinemaProfile : Profile
    {
        public CinemaProfile()
        {
            CreateMap<CreateCinemaDTO, Cinema>();
            CreateMap<Cinema, ReadCinemaDTO>();
            CreateMap<UpdateCinemaDTO, Cinema>();
        }
    }
}
