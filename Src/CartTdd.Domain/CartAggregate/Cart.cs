using CartTdd.Domain.Exceptions;

namespace CartTdd.Domain.CartAggregate
{
    public class Cart
    {
        public decimal TotalPrice => Products.Sum(p => p.TotalPrice);
        private readonly List<CartProduct> _products = new();
        public IReadOnlyList<CartProduct> Products => _products.AsReadOnly();

        public void AddProduct(CartProduct cartProduct)
        {
            _products.Add(cartProduct);
        }

        public void RemoveProduct(string name)
        {
            var productRemove = _products.Where(p => p.Name.Equals(name));
            if (!productRemove.Any()) {
                throw new CartProductIsNotFoundException();
            }
            _products.RemoveAll(p => p.Name.Equals(name));
        }
    }
}