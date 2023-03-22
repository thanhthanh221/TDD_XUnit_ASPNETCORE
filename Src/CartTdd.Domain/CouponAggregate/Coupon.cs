namespace CartTdd.Domain.CouponAggregate
{
    public class Coupon
    {
        public Coupon(string code, decimal amount, DateTime expired)
        {
            Id = Guid.NewGuid();
            Code = code;
            Amount = amount;
            Expired = expired;
        }
        public Guid Id {get; private set;}
        public string Code { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime Expired { get; private set; }

    }
}