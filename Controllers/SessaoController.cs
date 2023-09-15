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

        [HttpPost]
        public IActionResult AdicionaSessao(CreateSessaoDTO sessaoDTO)
        {
            Sessao sessao = _mapper.Map<Sessao>(sessaoDTO);
            _context.Sessoes.Add(sessao);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ConsultarSessoesPorId), new { Id = sessao.Id }, sessao);
        }

        [HttpGet]
        public IEnumerable<ReadSessaoDTO> ConsultarSessoes()
        {
            return _mapper.Map<List<ReadSessaoDTO>>(_context.Sessoes.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult ConsultarSessoesPorId(int id)
        {
            Sessao sessao = _context.Sessoes.FirstOrDefault(sessao => sessao.Id == id);
            if (sessao != null)
            {
                ReadSessaoDTO sessaoDTO = _mapper.Map<ReadSessaoDTO>(sessao);

                return Ok(sessaoDTO);
            }
            return NotFound();
        }

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
    }
}
