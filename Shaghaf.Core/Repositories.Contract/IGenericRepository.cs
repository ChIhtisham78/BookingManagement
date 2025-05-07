using System.Collections.Generic;
using System.Threading.Tasks;
using Shaghaf.Core.Entities;
using Shaghaf.Core.Specifications;

namespace Shaghaf.Core.Repositories.Contract
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdWithSpecAsync(ISpecifications<T> spec);
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec);
        Task<T?> GetEntityWithSpecAsync(ISpecifications<T> spec); // Add this method
        Task AddAsync(T entity); // Change void to Task
        void Update(T entity);
        void Delete(T entity);
    }

}
