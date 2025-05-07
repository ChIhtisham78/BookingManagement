using Shaghaf.Core.Entities.Cart_Entities;

namespace Shaghaf.Core.Repositories.Contract
{
    public interface ICartRepository
    {
        Task<CustomerCart?> GetCartAsync(string cartId);
        Task<CustomerCart?> UpdateCartAsync(CustomerCart cart);
        Task<bool> DeleteCartAsync(string cartId);
        Task<IEnumerable<CustomerCart>> GetAllCartsAsync();     
    }
}
