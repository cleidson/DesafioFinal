using DesafioFinal.Core.DTOs;
using DesafioFinal.Core.Logic.Interfaces.Repositories;
using DesafioFinal.Core.Logic.Interfaces.Services;
using DesafioFinal.Core.Models;

namespace DesafioFinal.Core.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;

        public ClienteService(IClienteRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Adiciona um novo cliente ao sistema.
        /// </summary>
        public async Task<ClienteDto> AddClienteAsync(ClienteDto clienteDto)
        {
            ValidarClienteDto(clienteDto);

            var cliente = new Cliente
            {
                Nome = clienteDto.Nome.Trim(),
                Email = clienteDto.Email.Trim()
            };

            var clienteCriado = await _repository.AddAsync(cliente);
            return MapearParaDto(clienteCriado);
        }

        /// <summary>
        /// Exclui um cliente pelo ID.
        /// </summary>
        public async Task DeleteClienteAsync(int id)
        {
            var cliente = await _repository.GetByIdAsync(id)
                          ?? throw new KeyNotFoundException($"Cliente com ID {id} não encontrado.");

            await _repository.DeleteAsync(id);
        }

        /// <summary>
        /// Atualiza um cliente existente.
        /// </summary>
        public async Task UpdateClienteAsync(ClienteDto clienteDto)
        {
            ValidarClienteDto(clienteDto);

            var cliente = await _repository.GetByIdAsync(clienteDto.Id)
                          ?? throw new KeyNotFoundException($"Cliente com ID {clienteDto.Id} não encontrado.");

            // Evita atualizar se não houver mudanças
            if (cliente.Nome == clienteDto.Nome.Trim() && cliente.Email == clienteDto.Email.Trim())
                return;

            cliente.Nome = clienteDto.Nome.Trim();
            cliente.Email = clienteDto.Email.Trim();

            await _repository.UpdateAsync(cliente);
        }

        /// <summary>
        /// Retorna todos os clientes cadastrados.
        /// </summary>
        public async Task<List<ClienteDto>> GetAllClientesAsync()
        {
            return await _repository.GetAllClientesAsync();
        }

        /// <summary>
        /// Pesquisa clientes pelo nome.
        /// </summary>
        public async Task<List<ClienteDto>> GetClientesByNameAsync(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome do cliente deve ser informado.");

            return await _repository.GetClientesByNameAsync(nome.Trim());
        }

        /// <summary>
        /// Retorna o total de clientes cadastrados.
        /// </summary>
        public async Task<int> GetTotalClientesAsync()
        {
            return await _repository.GetTotalClientesAsync();
        }

        #region Métodos Privados

        /// <summary>
        /// Valida se o DTO do cliente é válido.
        /// </summary>
        private static void ValidarClienteDto(ClienteDto clienteDto)
        {
            if (clienteDto == null)
                throw new ArgumentNullException(nameof(clienteDto));

            if (string.IsNullOrWhiteSpace(clienteDto.Nome))
                throw new ArgumentException("O nome do cliente deve ser informado.");

            if (string.IsNullOrWhiteSpace(clienteDto.Email))
                throw new ArgumentException("O email do cliente deve ser informado.");
        }

        /// <summary>
        /// Converte um Cliente para ClienteDto.
        /// </summary>
        private static ClienteDto MapearParaDto(Cliente cliente)
        {
            return new ClienteDto
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Email = cliente.Email
            };
        }

        #endregion
    }
}
