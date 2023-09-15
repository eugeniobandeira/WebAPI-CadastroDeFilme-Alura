using System.ComponentModel.DataAnnotations;

namespace FilmesAPI_Alura.Models
{
    public class Sessao
    {
        [Key]
        [Required]
        public int Id { get; set; }
    }
}
