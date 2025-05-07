using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Shaghaf.Core;
using Shaghaf.Core.Entities;
using Shaghaf.Core.Repositories.Contract;
using Shaghaf.Infrastructure.Data;
using Shaghaf.Infrastructure.Repositories.Implementation;
using System;
using System.Collections;
using System.Threading.Tasks;

namespace Shaghaf.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _dbcontext;
        private Hashtable _repositories;
        private IDbContextTransaction _currentTransaction;

        public UnitOfWork(StoreContext dbcontext)
        {
            _dbcontext = dbcontext;
            _repositories = new Hashtable();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var key = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(key))
            {
                var repository = new GenericRepository<TEntity>(_dbcontext);
                _repositories.Add(key, repository);
            }

            return _repositories[key] as IGenericRepository<TEntity>;
        }

        public async Task<int> CompleteAsync()
            {
            return await _dbcontext.SaveChangesAsync();
        }

        public IDbContextTransaction BeginTransaction()
        {
            if (_currentTransaction != null)
            {
                return null;
            }
            _currentTransaction = _dbcontext.Database.BeginTransaction();
            return _currentTransaction;
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await _dbcontext.SaveChangesAsync();
                _currentTransaction?.Commit();
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    await _currentTransaction.DisposeAsync();
                    _currentTransaction = null;
                }
            }
        }

        public async Task RollbackTransactionAsync()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    await _currentTransaction.DisposeAsync();
                    _currentTransaction = null;
                }
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.DisposeAsync();
            }
            await _dbcontext.DisposeAsync();
        }
    }
}
