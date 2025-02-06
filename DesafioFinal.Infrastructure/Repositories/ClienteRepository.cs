using DesafioFinal.Core.DTOs;
using DesafioFinal.Core.Logic.Interfaces.Repositories;
using DesafioFinal.Core.Models;
using DesafioFinal.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DesafioFinal.Infrastructure.Repositories
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        private readonly DesafioFinalContext _context;
        private readonly DbSet<Cliente> _dbSet;

        public ClienteRepository(DesafioFinalContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = context.Set<Cliente>();
        }

        /// <summary>
        /// Retorna todos os clientes como DTOs.
        /// </summary>
        public async Task<List<ClienteDto>> GetAllClientesAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .Select(c => new ClienteDto
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Email = c.Email
                }).OrderBy(c => c.Id)
                .ToListAsync();
        }

        /// <summary>
        /// Retorna clientes filtrando pelo nome (ignora maiúsculas/minúsculas).
        /// </summary>
        public async Task<List<ClienteDto>> GetClientesByNameAsync(string nome)
        {
            
            nome = nome.ToLower().Trim();

            return await _dbSet
                .AsNoTracking()
                .Where(c => c.Nome.ToLower().Contains(nome))
                .Select(c => new ClienteDto
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Email = c.Email
                })
                .ToListAsync();
        }

        /// <summary>
        /// Retorna o total de clientes cadastrados.
        /// </summary>
        public async Task<int> GetTotalClientesAsync()
        {
            return await _dbSet.CountAsync();
        }
    }
}