﻿
namespace FilmesAPI_Alura.Data.DTOs
{
    public class ReadCinemaDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public ReadEnderecoDTO ReadEnderecoDTO { get; set; } //Faz parte do relacionamento
    }
}
