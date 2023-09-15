using AutoMapper;
using FilmesAPI_Alura.Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using FilmesAPI_Alura.Data;
using FilmesAPI_Alura.Models;

namespace FilmesAPI_Alura.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CinemaController : ControllerBase
    {
        private FilmeContext _context;
        private IMapper _mapper;

        public CinemaController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpPost]
        public IActionResult AdicionaCinema([FromBody] CreateCinemaDTO cinemaDTO)
        {
            Cinema cinema = _mapper.Map<Cinema>(cinemaDTO);
            _context.Cinemas.Add(cinema);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ConsultarCinemasPorId), new { Id = cinema.Id }, cinemaDTO);
        }

        [HttpGet]
        public IEnumerable<ReadCinemaDTO> ConsultarCinemas()
        {
            var listaDeCinemas = _mapper.Map<List<ReadCinemaDTO>>(_context.Cinemas.ToList());
            return listaDeCinemas;
        }


        [HttpGet("{id}")]
        public IActionResult ConsultarCinemasPorId(int id)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema != null)
            {
                ReadCinemaDTO cinemaDTO = _mapper.Map<ReadCinemaDTO>(cinema);
                return Ok(cinemaDTO);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaCinema(int id, [FromBody] UpdateCinemaDTO cinemaDTO)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema == null)
            {
                return NotFound();
            }
            _mapper.Map(cinemaDTO, cinema);
            _context.SaveChanges();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult DeletarCinema(int id)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema == null)
            {
                return NotFound();
            }
            _context.Remove(cinema);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
