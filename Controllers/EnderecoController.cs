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

        /// <summary>
        /// Adiciona um endereço ao banco de dados
        /// </summary>
        /// <param name="enderecoDTO">Objeto com os campos necessários para criação de um filme</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso inserção seja feita com sucesso</response>
        [HttpPost]
        public IActionResult AdicionaEndereco([FromBody] CreateEnderecoDTO enderecoDTO)
        {
            Endereco endereco = _mapper.Map<Endereco>(enderecoDTO);
            _context.Enderecos.Add(endereco);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ConsultarEnderecosPorId), new { Id = endereco.Id }, enderecoDTO);
        }

        /// <summary>
        /// Recupera uma lista de endereços do banco de dados
        /// </summary>
        /// <returns>Informações dos endereços buscados</returns>
        /// <response code="200">Com a lista de endereços presentes na base de dados</response>
        [HttpGet]
        public IEnumerable<ReadEnderecoDTO> ConsultarEndereco()
        {
            return (IEnumerable<ReadEnderecoDTO>)_mapper.Map<List<ReadEnderecoDTO>>(_context.Enderecos.ToList());
        }

        /// <summary>
        /// Recupera um endereço no banco de dados usando seu id
        /// </summary>
        /// <param name="id">Id do endereço a ser recuperado no banco</param>
        /// <returns>Informações do endereço buscado</returns>
        /// <response code="200">Caso o id seja existente na base de dados</response>
        /// <response code="404">Caso o id seja inexistente na base de dados</response>
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

        /// <summary>
        /// Atualiza um endereço no banco de dados usando seu id
        /// </summary>
        /// <param name="id">Id do endereço a ser atualizado no banco</param>
        /// <param name="enderecoDTO">Objeto com os campos necessários para atualização de um endereço</param>
        /// <returns>Sem conteúdo de retorno</returns>
        /// <response code="204">Caso o id seja existente na base de dados e o endereço tenha sido atualizado</response>
        /// <response code="404">Caso o id seja inexistente na base de dados</response>
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


        /// <summary>
        /// Deleta um endereço do banco de dados usando seu id
        /// </summary>
        /// <param name="id">Id do endereço a ser removido do banco</param>
        /// <returns>Sem conteúdo de retorno</returns>
        /// <response code="204">Caso o id seja existente na base de dados e o endereço tenha sido removido</response>
        /// <response code="404">Caso o id seja inexistente na base de dados</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
