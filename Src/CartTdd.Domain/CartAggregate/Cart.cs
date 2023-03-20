using CartTdd.Domain.Exceptions;

namespace CartTdd.Domain.CartAggregate
{
    public class Cart
    {
        public decimal TotalPrice => Products.Sum(p => p.TotalPrice) - (Coupon?.Amount).GetValueOrDefault(0);
        private readonly List<CartProduct> _products = new();
        public IReadOnlyList<CartProduct> Products => _products.AsReadOnly();
        public CartCoupon Coupon { get; set; }

        public void AddProduct(CartProduct cartProduct)
        {
            _products.Add(cartProduct);
        }

        public void RemoveProduct(string name)
        {
            var productRemove = _products.Where(p => p.Name.Equals(name));
            if (!productRemove.Any())
            {
                throw new CartProductIsNotFoundException();
            }
            _products.RemoveAll(p => p.Name.Equals(name));
        }

        public void IncreaseProductQuantity(string name)
        {
            var product = _products.FirstOrDefault(p => p.Name == name);

            if (product is null) throw new CartProductIsNotFoundException();
            else product.IncreaseProduct();
        }

        public void DecreaseProductQuantity(string name)
        {
            var product = _products.FirstOrDefault(p => p.Name == name);

            if (product is null) throw new CartProductIsNotFoundException();
            else if (product.Quantity == 1) _products.Remove(product);
            else product.DecreaseProduct();
        }

        public void ClearProducts() => _products.Clear();

        public void ApplyCoupon(CartCoupon cartCoupon)
        {
            Coupon = cartCoupon;
        }
    }
}