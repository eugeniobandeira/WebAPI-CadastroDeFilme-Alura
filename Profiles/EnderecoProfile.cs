using AutoMapper;
using FilmesAPI_Alura.Data.DTOs;
using FilmesAPI_Alura.Models;

namespace FilmesAPI_Alura.Profiles
{
    public class EnderecoProfile : Profile
    {
        public EnderecoProfile()
        {
            CreateMap<CreateEnderecoDTO, Endereco>();
            CreateMap<Endereco, ReadEnderecoDTO>();
            CreateMap<UpdateEnderecoDTO, Endereco>();
        }
    }
}
