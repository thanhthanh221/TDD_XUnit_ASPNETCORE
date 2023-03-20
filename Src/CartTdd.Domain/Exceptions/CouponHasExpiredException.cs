namespace CartTdd.Domain.Exceptions
{
    public class CouponHasExpiredException : Exception
    {
        public CouponHasExpiredException() : base("Coupon has expired")
        {
        }

    }
}