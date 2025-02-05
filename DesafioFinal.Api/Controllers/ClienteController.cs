using DesafioFinal.Core.DTOs;
using DesafioFinal.Core.Logic.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace DesafioFinal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)] 
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        #region CRUD

        /// <summary>
        /// Retorna um cliente pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDto>> GetByIdAsync(int id)
        {
            try
            {
                var cliente = await _clienteService.GetAllClientesAsync();
                var clienteEncontrado = cliente.FirstOrDefault(c => c.Id == id);

                if (clienteEncontrado == null)
                    return NotFound($"Cliente com ID {id} não encontrado.");

                return Ok(clienteEncontrado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao buscar cliente: {ex.Message}");
            }
        }

        /// <summary>
        /// Adiciona um novo cliente.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ClienteDto>> CreateAsync([FromBody] ClienteDto clienteDto)
        {
            try
            {
                var clienteCriado = await _clienteService.AddClienteAsync(clienteDto);

                return CreatedAtAction(nameof(GetByIdAsync), new { id = clienteCriado.Id }, clienteCriado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao adicionar cliente. Tente novamente mais tarde.");
            }
        }


        /// <summary>
        /// Atualiza um cliente existente.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] ClienteDto clienteDto)
        {
            try
            {
                if (clienteDto == null || clienteDto.Id != id)
                    return BadRequest("Dados inválidos.");

                await _clienteService.UpdateClienteAsync(clienteDto);
                return NoContent(); // Sucesso sem retorno de conteúdo
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar cliente: {ex.Message}");
            }
        }

        /// <summary>
        /// Remove um cliente pelo ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _clienteService.DeleteClienteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao remover cliente: {ex.Message}");
            }
        }

        #endregion

        #region Métodos Adicionais

        /// <summary>
        /// Retorna o total de clientes cadastrados.
        /// </summary>
        [HttpGet("count")]
        public async Task<ActionResult<int>> GetTotalClientesAsync()
        {
            try
            {
                var totalClientes = await _clienteService.GetTotalClientesAsync();
                return Ok(totalClientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter total de clientes: {ex.Message}");
            }
        }

        /// <summary>
        /// Retorna todos os clientes cadastrados.
        /// </summary>
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<ClienteDto>>> GetAllAsync()
        {
            try
            {
                var clientes = await _clienteService.GetAllClientesAsync();

                if (clientes == null || !clientes.Any())
                    return NoContent(); // Retorna 204 caso não haja clientes

                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter clientes: {ex.Message}");
            }
        }

        /// <summary>
        /// Pesquisa clientes pelo nome.
        /// </summary>
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ClienteDto>>> SearchByNameAsync([FromQuery] string nome)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nome))
                    return BadRequest("O nome do cliente deve ser informado.");

                var clientesFiltrados = await _clienteService.GetClientesByNameAsync(nome.ToLower());

                if (!clientesFiltrados.Any())
                    return NotFound($"Nenhum cliente encontrado para o nome '{nome}'.");

                return Ok(clientesFiltrados);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar clientes: {ex.Message}");
            }
        }

        #endregion
    }
}
