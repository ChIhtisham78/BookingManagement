using Shaghaf.Core.Entities.Cart_Entities;
using Shaghaf.Core.Repositories.Contract;
using StackExchange.Redis;
using System.Text.Json;

public class CartRepository : ICartRepository
{
    private readonly IDatabase _database;
    private readonly IConnectionMultiplexer _redisConnection;

    public CartRepository(IConnectionMultiplexer redisConnection)
    {
        _redisConnection = redisConnection;
        _database = redisConnection.GetDatabase();
    }

    public async Task<CustomerCart?> GetCartAsync(string cartId)
    {
        var cart = await _database.StringGetAsync(cartId);
        return cart.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerCart>(cart);
    }

    public async Task<CustomerCart?> UpdateCartAsync(CustomerCart cart)
    {
        var serializedCart = JsonSerializer.Serialize(cart);
        var createdOrUpdated = await _database.StringSetAsync(cart.Id, serializedCart, TimeSpan.FromDays(30));

        if (!createdOrUpdated) return null;
        return await GetCartAsync(cart.Id);
    }

    public async Task<bool> DeleteCartAsync(string cartId)
    {
        return await _database.KeyDeleteAsync(cartId);
    }

    public async Task<IEnumerable<CustomerCart>> GetAllCartsAsync()
    {
        var server = _redisConnection.GetServer(_redisConnection.GetEndPoints().First());
        var keys = server.Keys(database: _database.Database, pattern: "*");

        var carts = new List<CustomerCart>();

        foreach (var key in keys)
        {
            var cart = await _database.StringGetAsync(key);
            if (!cart.IsNullOrEmpty)
            {
                carts.Add(JsonSerializer.Deserialize<CustomerCart>(cart));
            }
        }

        return carts;
    }
}
