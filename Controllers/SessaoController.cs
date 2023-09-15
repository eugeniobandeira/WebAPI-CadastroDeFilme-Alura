using AutoMapper;
using FilmesAPI_Alura.Data.DTOs;
using FilmesAPI_Alura.Data;
using FilmesAPI_Alura.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI_Alura.Controllers
{
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
        public IActionResult AdicionaSessao([FromBody] CreateSessaoDTO sessaoDTO)
        {
            Sessao sessao = _mapper.Map<Sessao>(sessaoDTO);
            _context.Sessoes.Add(sessao);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ConsultarSessaoPorId), new { Id = sessao.Id }, sessaoDTO);
        }

        [HttpGet]
        public IEnumerable<ReadSessaoDTO> ConsultarSessao()
        {
            return (IEnumerable<ReadSessaoDTO>)_mapper.Map<List<ReadSessaoDTO>>(_context.Sessoes.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult ConsultarSessaoPorId(int id)
        {
            Sessao sessao = _context.Sessoes.FirstOrDefault(sessao => sessao.Id == id);
            if (sessao != null)
            {
                ReadSessaoDTO sessaoDTO = _mapper.Map<ReadSessaoDTO>(sessao);
                return Ok(sessaoDTO);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaSessao(int id, [FromBody] UpdateSessaoDTO sessaoDTO)
        {
            Sessao sessao = _context.Sessoes.FirstOrDefault(sessao => sessao.Id == id);
            if (sessao == null)
            {
                return NotFound();
            }
            _mapper.Map(sessaoDTO, sessao);
            _context.SaveChanges();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult DeletarSessao(int id)
        {
            Sessao sessao = _context.Sessoes.FirstOrDefault(sessao => sessao.Id == id);
            if (sessao == null)
            {
                return NotFound();
            }
            _context.Remove(sessao);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
