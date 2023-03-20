namespace CartTdd.Domain.CartAggregate
{
    public class CartProduct
    {
        public CartProduct(string name, int quantity, decimal price)
        {
            Name = name;
            Quantity = quantity;
            Price = price;
        }

        public string Name { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }
        public decimal TotalPrice => Quantity * Price;

        // Cùng 1 dll mới truy cập được
        internal void IncreaseProduct() => Quantity++;
        internal void DecreaseProduct() => Quantity--;

    }
}