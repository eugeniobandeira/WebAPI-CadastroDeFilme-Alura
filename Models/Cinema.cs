using System.ComponentModel.DataAnnotations;

namespace FilmesAPI_Alura.Models
{
    public class Cinema
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        public string Nome { get; set; }

        //Criando o relacionamento entre Cinema e Endereço
        public int EnderecoId { get; set; } //Para que o cinema seja cadastrado no BD, deve-se informar o EnderecoId
        //Relacionamento 1:1
        public virtual Endereco  Endereco { get; set; } //É preciso fazer a mesma coisa no Model Endereco
        //Virtual indica cardinalidade e permite recuperar uma instância do seu respectivo endereço
        public virtual ICollection<Sessao> Sessoes { get; set; }

    }
}
