namespace CartTdd.Domain.Exceptions
{
    public class CouponIsNotFoundException : Exception
    {
        public CouponIsNotFoundException() : base("Coupon is not found")
        {
        }
    }
}