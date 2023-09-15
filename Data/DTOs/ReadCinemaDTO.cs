
namespace FilmesAPI_Alura.Data.DTOs
{
    public class ReadCinemaDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public ReadEnderecoDTO ReadEndereco { get; set; } //Faz parte do relacionamento
        public ICollection<ReadSessaoDTO> Sessoes { get; set; }
    }
}
