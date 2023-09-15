using System.ComponentModel.DataAnnotations;

namespace FilmesAPI_Alura.Models
{
    public class Endereco
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }

        //Criando o relacionamento entre Endereco e Cinema
        public virtual Cinema Cinema { get; set; }
    }
}
