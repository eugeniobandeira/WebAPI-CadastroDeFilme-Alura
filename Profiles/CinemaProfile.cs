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

            //Mapeando para pegar o Endereço (virtual) que está na model Cinema
            CreateMap<Cinema, ReadCinemaDTO>()
                .ForMember(cinemaDTO => cinemaDTO.ReadEndereco, 
                opt => opt.MapFrom(cinema => cinema.Endereco))
                .ForMember(cinemaDTO => cinemaDTO.Sessoes,
                opt => opt.MapFrom(cinema => cinema.Sessoes));

            CreateMap<UpdateCinemaDTO, Cinema>();
        }
    }
}
