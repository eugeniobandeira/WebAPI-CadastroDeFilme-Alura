using AutoMapper;
using FilmesAPI_Alura.Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using FilmesAPI_Alura.Data;
using FilmesAPI_Alura.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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

        /// <summary>
        /// Adiciona um cinema ao banco de dados
        /// </summary>
        /// <param name="cinemaDTO">Objeto com os campos necessários para criação de um filme</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso inserção seja feita com sucesso</response>
        [HttpPost]
        public IActionResult AdicionaCinema([FromBody] CreateCinemaDTO cinemaDTO)
        {
            Cinema cinema = _mapper.Map<Cinema>(cinemaDTO);
            _context.Cinemas.Add(cinema);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ConsultarCinemasPorId), new { Id = cinema.Id }, cinemaDTO);
        }

        /// <summary>
        /// Recupera uma lista de cinemas do banco de dados
        /// </summary>
        /// <returns>Informações dos cinemas buscados</returns>
        /// <response code="200">Com a lista de cinemas presentes na base de dados</response>
        [HttpGet]
        public IEnumerable<ReadCinemaDTO> ConsultarCinemas([FromQuery] int? enderecoId = null)
        {
            if(enderecoId == null)
            {
                return _mapper.Map<List<ReadCinemaDTO>>(_context.Cinemas.ToList());
            }
            return _mapper.Map<List<ReadCinemaDTO>>(_context.Cinemas.FromSqlRaw(
                $"SELECT Nome, Id, EnderecoId FROM cinemas WHERE cinemas.EnderecoId = {enderecoId}").ToList());
        }

        /// <summary>
        /// Recupera um cinema no banco de dados usando seu id
        /// </summary>
        /// <param name="id">Id do cinema a ser recuperado no banco</param>
        /// <returns>Informações do cinema buscado</returns>
        /// <response code="200">Caso o id seja existente na base de dados</response>
        /// <response code="404">Caso o id seja inexistente na base de dados</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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


        /// <summary>
        /// Atualiza um cinema no banco de dados usando seu id
        /// </summary>
        /// <param name="id">Id do cinema a ser atualizado no banco</param>
        /// <param name="cinemaDTO">Objeto com os campos necessários para atualização de um cinema</param>
        /// <returns>Sem conteúdo de retorno</returns>
        /// <response code="204">Caso o id seja existente na base de dados e o cinema tenha sido atualizado</response>
        /// <response code="404">Caso o id seja inexistente na base de dados</response>
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


        /// <summary>
        /// Deleta um cinema do banco de dados usando seu id
        /// </summary>
        /// <param name="id">Id do cinema a ser removido do banco</param>
        /// <returns>Sem conteúdo de retorno</returns>
        /// <response code="204">Caso o id seja existente na base de dados e o cinema tenha sido removido</response>
        /// <response code="404">Caso o id seja inexistente na base de dados</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
