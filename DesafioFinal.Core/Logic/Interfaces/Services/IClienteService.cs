
using DesafioFinal.Core.DTOs;

namespace DesafioFinal.Core.Logic.Interfaces.Services
{
    public interface IClienteService
    {
        Task<ClienteDto> AddClienteAsync(ClienteDto cliente); 
        Task UpdateClienteAsync(ClienteDto cliente);
        Task DeleteClienteAsync(int id);
        Task<int> GetTotalClientesAsync(); 
        Task<List<ClienteDto>> GetAllClientesAsync(); 
        Task<List<ClienteDto>> GetClientesByNameAsync(string nome); 
    }

}
