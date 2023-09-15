using System.ComponentModel.DataAnnotations;

namespace FilmesAPI_Alura.Data.DTOs
{
    public class CreateCinemaDTO
    {
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        public string Nome { get; set; }
        public int EnderecoId { get; set; } //Para criar o relacionamento
    }
}
