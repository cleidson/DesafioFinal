﻿using System.Linq.Expressions;


namespace DesafioFinal.Core.Logic.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);  
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }

}
