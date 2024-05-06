using System;
using System.Security.Principal;
using eMKParty.BackOffice.Support.Domain.Common.Interfaces;

namespace eMKParty.BackOffice.Support.Application.Interfaces.Repositories
{
	public interface IGenericRepository<T> where T : class, IEntity
    {
        IQueryable<T> Entities { get; }

        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}