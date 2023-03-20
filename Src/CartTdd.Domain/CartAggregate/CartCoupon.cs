namespace CartTdd.Domain.CartAggregate
{
    public class CartCoupon
    {
        public CartCoupon(string code, decimal amount)
        {
            Code = code;
            Amount = amount;
        }

        public string Code { get; private set; }
        public decimal Amount { get; private set; }

    }
}