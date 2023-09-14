﻿using System.ComponentModel.DataAnnotations;

namespace FilmesAPI_Alura.Data.DTOs
{
    public class CreateFilmeDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O titulo do filme é obrigatório")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O gênero do filme é obrigatório")]
        [MaxLength(50, ErrorMessage = "O tamanho do gênero não pode exceder 50 caracteres")]
        public string Genero { get; set; }

        [Required(ErrorMessage = "A duração do filme é obrigatória")]
        [Range(70, 600, ErrorMessage = "A duração deve estar entre 70 e 600 minutos")]
        public int Duracao { get; set; }
    }
}
