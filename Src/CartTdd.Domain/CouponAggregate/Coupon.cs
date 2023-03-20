namespace CartTdd.Domain.CouponAggregate
{
    public class Coupon
    {
        public Coupon(string code, decimal amount, DateTime expired)
        {
            Code = code;
            Amount = amount;
            Expired = expired;
        }

        public string Code { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime Expired { get; private set; }

    }
}