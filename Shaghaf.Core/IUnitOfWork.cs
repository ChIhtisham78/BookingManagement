using Microsoft.EntityFrameworkCore.Storage;
using Shaghaf.Core.Entities;
using Shaghaf.Core.Repositories.Contract;
using System;

namespace Shaghaf.Core
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        Task<int> CompleteAsync();

        // Transaction management methods
        IDbContextTransaction BeginTransaction();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
