namespace CartTdd.Domain.CartAggregate;
    public interface ICartRepository
    {
        Task<Cart> GetAsync(Guid id);
        Task UpdateAsync(Cart cart);
    }
