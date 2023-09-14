using FilmesAPI_Alura.Data;
using FilmesAPI_Alura.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI_Alura.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmeController : ControllerBase
    {
        //private static List<Filme> filmes = new List<Filme>();
        //private static int Id = 1;
        private FilmeContext _context;

        public FilmeController(FilmeContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AdicionarFilme([FromBody] Filme filme) //Recebe as informações (do filme) do corpo da requisição
        {
            _context.Filmes.Add(filme); //O Filme vem do DBSet
            _context.SaveChanges();
            return CreatedAtAction(nameof(ConsultarPorId), new { id = filme.Id }, filme); //retorna o filme cadastrado
            //nameof é o método que consulta filme
            //new é o id do filme que acabou de ser cadastrado
            //filme é o objeto que foi criado
        }

        [HttpGet]
        public IEnumerable<Filme>ConsultarFilmes([FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            //return filmes;
            return _context.Filmes.Skip(skip).Take(take);

            //Vamos supor que tenha mais de 100 filmes, porém não precisamos exibir todos
            //Nesse contexto, o usuário pode informar quantos deseja pular e quantos deseja ver
            //Caso ele não faça isso, setamos valores padrões para o cenário
        }

        [HttpGet("{id}")]
        public IActionResult ConsultarPorId(int id) { 
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if(filme == null)
            {
                return NotFound();
            }
            return Ok(filme);
        }
    }
}
