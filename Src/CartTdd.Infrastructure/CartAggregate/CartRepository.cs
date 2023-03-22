using CartTdd.Domain.CartAggregate;
using CartTdd.Infrastructure.DataBase;
using MongoDB.Driver;

namespace CartTdd.Infrastructure.CartAggregate;
public class CartRepository : ICartRepository
{
    private readonly DbContext dbContext;

    public CartRepository(DbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Cart> GetAsync(Guid id)
    {
        var filter = Builders<Cart>.Filter.Eq(c => c.Id, id);
        var documents = await dbContext.Carts.FindAsync(filter);

        return await documents.FirstOrDefaultAsync();

    }

    public async Task UpdateAsync(Cart cart)
    {
        var filter = Builders<Cart>.Filter.Eq(c => c.Id, cart.Id);
        var update = Builders<Cart>.Update.Set(c => c.Coupon, cart.Coupon);

        await dbContext.Carts.UpdateOneAsync(filter, update);
    }
}