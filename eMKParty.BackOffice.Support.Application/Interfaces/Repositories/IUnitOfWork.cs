using System;
using eMKParty.BackOffice.Support.Domain.Common;

namespace eMKParty.BackOffice.Support.Application.Interfaces.Repositories
{
	public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> Repository<T>() where T : BaseAuditableEntity;
        Task<int> Save(CancellationToken cancellationToken);
        Task<int> SaveAndRemoveCache(CancellationToken cancellationToken, params string[] cacheKeys);
        Task Rollback();
    }
}