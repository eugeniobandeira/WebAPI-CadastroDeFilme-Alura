using AutoMapper;
using FilmesAPI_Alura.Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using FilmesAPI_Alura.Data;
using FilmesAPI_Alura.Models;

namespace FilmesAPI_Alura.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class EnderecoController : ControllerBase
    {
        private FilmeContext _context;
        private IMapper _mapper;

        public EnderecoController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpPost]
        public IActionResult AdicionaEndereco([FromBody] CreateEnderecoDTO enderecoDTO)
        {
            Endereco endereco = _mapper.Map<Endereco>(enderecoDTO);
            _context.Enderecos.Add(endereco);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ConsultarEnderecosPorId), new { Id = endereco.Id }, enderecoDTO);
        }

        [HttpGet]
        public IEnumerable<ReadCinemaDTO> ConsultarEndereco()
        {
            return (IEnumerable<ReadCinemaDTO>)_mapper.Map<List<ReadEnderecoDTO>>(_context.Enderecos.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult ConsultarEnderecosPorId(int id)
        {
            Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
            if (endereco != null)
            {
                ReadEnderecoDTO enderecoDTO = _mapper.Map<ReadEnderecoDTO>(endereco);
                return Ok(enderecoDTO);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaEndereco(int id, [FromBody] UpdateEnderecoDTO enderecoDTO)
        {
            Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
            if (endereco == null)
            {
                return NotFound();
            }
            _mapper.Map(enderecoDTO, endereco);
            _context.SaveChanges();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult DeletarEndereco(int id)
        {
            Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
            if (endereco == null)
            {
                return NotFound();
            }
            _context.Remove(endereco);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
