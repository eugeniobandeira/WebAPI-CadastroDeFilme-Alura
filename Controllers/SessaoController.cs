using AutoMapper;
using FilmesAPI_Alura.Data.DTOs;
using FilmesAPI_Alura.Data;
using FilmesAPI_Alura.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI_Alura.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessaoController : ControllerBase
    {
        private FilmeContext _context;
        private IMapper _mapper;

        public SessaoController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Adiciona uma sessão ao banco de dados
        /// </summary>
        /// <param name="sessaoDTO">Objeto com os campos necessários para criação de um filme</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso inserção seja feita com sucesso</response>
        [HttpPost]
        public IActionResult AdicionaSessao(CreateSessaoDTO sessaoDTO)
        {
            Sessao sessao = _mapper.Map<Sessao>(sessaoDTO);
            _context.Sessoes.Add(sessao);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ConsultarSessoesPorId),
                new { filmeId = sessao.FilmeId, cinemaId = sessao.CinemaId }, sessao);
        }

        /// <summary>
        /// Recupera uma lista de sessões do banco de dados
        /// </summary>
        /// <returns>Informações dos endereços buscados</returns>
        /// <response code="200">Com a lista de sessões presentes na base de dados</response>
        [HttpGet]
        public IEnumerable<ReadSessaoDTO> ConsultarSessoes()
        {
            return _mapper.Map<List<ReadSessaoDTO>>(_context.Sessoes.ToList());
        }


        /// <summary>
        /// Recupera uma sessão no banco de dados usando seu filmeId e cinemaId
        /// </summary>
        /// <param name="filmeId">Id do filme a ser recuperado no banco</param>
        /// <param name="cinemaId">Id do cinema a ser recuperado no banco</param>
        /// <returns>Informações da sessão buscada</returns>
        /// <response code="200">Caso o filmeId, cinemaId sejam existentes na base de dados</response>
        /// <response code="404">Caso o filmeId, cinemaId sejam inexistente na base de dados</response>
        [HttpGet("{filmeId}/{cinemaId}")]
        public IActionResult ConsultarSessoesPorId(int filmeId, int cinemaId)
        {
            Sessao sessao = _context.Sessoes
                .FirstOrDefault(sessao => sessao.FilmeId == filmeId && sessao.CinemaId == cinemaId);
            if (sessao != null)
            {
                ReadSessaoDTO sessaoDTO = _mapper.Map<ReadSessaoDTO>(sessao);

                return Ok(sessaoDTO);
            }
            return NotFound();
        }
        
        /*
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult ExcluirSessao(int id)
        {
            Sessao sessao = _context.Sessoes.FirstOrDefault(sessao => sessao.Id == id);
            if (sessao != null)
            {
                _context.Remove(sessao);
                _context.SaveChanges();
                return NoContent();
            }
            return NotFound();
        }
        */
        
    }
}
