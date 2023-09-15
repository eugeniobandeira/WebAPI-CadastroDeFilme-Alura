﻿using System.ComponentModel.DataAnnotations;

namespace FilmesAPI_Alura.Data.DTOs
{
    public class CreateSessaoDTO
    {
        [Required]
        public int FilmeId { get; set; }
        public int CinemaId { get; set; }
    }
}
