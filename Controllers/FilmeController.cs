using AutoMapper;
using FilmesAPI_Alura.Data;
using FilmesAPI_Alura.Data.DTOs;
using FilmesAPI_Alura.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI_Alura.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmeController : ControllerBase
    {
        private FilmeContext _context;
        private IMapper _mapper;

        public FilmeController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        /// <summary>
        /// Adiciona um filme ao banco de dados
        /// </summary>
        /// <param name="filmeDto">Objeto com os campos necessários para criação de um filme</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso inserção seja feita com sucesso</response>
        [HttpPost]
        public IActionResult AdicionarFilme([FromBody] UpdateFilmeDTO filmeDTO)
        {
            Filme filme = _mapper.Map<Filme>(filmeDTO);
            _context.Filmes.Add(filme);
            _context.SaveChanges(); 
            return CreatedAtAction(nameof(ConsultarPorId), 
                new { id = filme.Id }, 
                filme);
        }


        /// <summary>
        /// Recupera uma lista de filmes do banco de dados
        /// </summary>
        /// <param name="skip">Número de filmes que serão pulados</param>
        /// <param name="take">Número de filmes que serão exibidos por página</param>
        /// <returns>Informações dos filmes buscados</returns>
        /// <response code="200">Com a lista de filmes presentes na base de dados</response>
        [HttpGet]
        public IEnumerable<ReadFilmeDTO> ConsultarFilmes([FromQuery] int skip = 0,
        [FromQuery] int take = 10)
        {
            return _mapper.Map<List<ReadFilmeDTO>>(_context.Filmes.Skip(skip).Take(take).ToList());
        }


        /// <summary>
        /// Recupera um filme no banco de dados usando seu id
        /// </summary>
        /// <param name="id">Id do filme a ser recuperado no banco</param>
        /// <returns>Informações do filme buscado</returns>
        /// <response code="200">Caso o id seja existente na base de dados</response>
        /// <response code="404">Caso o id seja inexistente na base de dados</response>
        [HttpGet("{id}")]
        public IActionResult ConsultarPorId(int id) {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null) return NotFound();
            var filmeDTO = _mapper.Map<ReadFilmeDTO>(filme);
            return Ok(filmeDTO);
        }


        /// <summary>
        /// Atualiza um filme no banco de dados usando seu id
        /// </summary>
        /// <param name="id">Id do filme a ser atualizado no banco</param>
        /// <param name="filmeDto">Objeto com os campos necessários para atualização de um filme</param>
        /// <returns>Sem conteúdo de retorno</returns>
        /// <response code="204">Caso o id seja existente na base de dados e o filme tenha sido atualizado</response>
        /// <response code="404">Caso o id seja inexistente na base de dados</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDTO filmeDTO)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme != null)
            {
                _mapper.Map(filmeDTO, filme);
                _context.SaveChanges();
                return NoContent();
            }
            return NotFound();
        }


        /// <summary>
        /// Atualiza valores parciais da obejto filme no banco de dados usando seu id
        /// </summary>
        /// <param name="id">Id do filme a ser atualizado no banco</param>
        /// <param name="op">Tipo de operação</param>
        /// <param name="path">'/nome do atributo que deseja alterar o valor' - Ex '/titulo'</param>
        /// <param name="value"> novo valor</param>
        /// <returns>Sem conteúdo de retorno</returns>
        /// <response code="204">Caso o id seja existente na base de dados e o filme tenha sido atualizado</response>
        /// <response code="404">Caso o id seja inexistente na base de dados</response>
        [HttpPatch("{id}")]
        public IActionResult AtualizarFilmeParcil(int id, JsonPatchDocument<UpdateFilmeDTO> patch)
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null) return NotFound();

            var filmeParaAtualizar = _mapper.Map<UpdateFilmeDTO>(filme);
            patch.ApplyTo(filmeParaAtualizar, ModelState);

            if(!TryValidateModel(filmeParaAtualizar))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(filmeParaAtualizar, filme);
            _context.SaveChanges();
            return NoContent();
        }


        /// <summary>
        /// Deleta um filme do banco de dados usando seu id
        /// </summary>
        /// <param name="id">Id do filme a ser removido do banco</param>
        /// <returns>Sem conteúdo de retorno</returns>
        /// <response code="204">Caso o id seja existente na base de dados e o filme tenha sido removido</response>
        /// <response code="404">Caso o id seja inexistente na base de dados</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult ExcluirFilme(int id)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme != null)
            {
                _context.Remove(filme);
                _context.SaveChanges();
                return NoContent();
            }
            return NotFound();
        }
    }
}
