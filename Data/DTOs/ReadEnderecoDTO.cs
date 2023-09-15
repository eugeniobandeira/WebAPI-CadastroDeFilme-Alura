﻿namespace FilmesAPI_Alura.Data.DTOs
{
    public class ReadEnderecoDTO
    {
        public int Id { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public ICollection<ReadSessaoDTO> Sessoes { get; set; }
    }
}
