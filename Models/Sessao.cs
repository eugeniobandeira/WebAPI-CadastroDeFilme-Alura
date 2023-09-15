using System.ComponentModel.DataAnnotations;

namespace FilmesAPI_Alura.Models
{
    public class Sessao
    {
        [Key]
        [Required]
        public int Id { get; set; }

        //Criando o relacionamento entre Sessão e Filme
        [Required]
        public int FilmeId { get; set; }
        public virtual Filme filme { get; set; }
    }
}
