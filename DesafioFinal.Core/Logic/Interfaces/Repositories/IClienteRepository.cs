using DesafioFinal.Core.DTOs;
using DesafioFinal.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DesafioFinal.Core.Logic.Interfaces.Repositories
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<List<ClienteDto>> GetAllClientesAsync();
        Task<List<ClienteDto>> GetClientesByNameAsync(string nome);
        Task<int> GetTotalClientesAsync();
    }


}
