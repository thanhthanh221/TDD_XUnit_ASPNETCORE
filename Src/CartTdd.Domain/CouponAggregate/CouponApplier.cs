using CartTdd.Domain.CartAggregate;
using CartTdd.Domain.Exceptions;

namespace CartTdd.Domain.CouponAggregate
{
    public class CouponApplier
    {
        private readonly ICouponRepository couponRepository;

        public CouponApplier(ICouponRepository couponRepository)
        {
            this.couponRepository = couponRepository;
        }

        public async Task Apply(string code, Cart cart)
        {
            var coupon = await couponRepository.GetAsync(code);
            if (coupon is null) throw new CouponIsNotFoundException();
            if(coupon.Expired < DateTime.Now) throw new CouponHasExpiredException();

            cart.ApplyCoupon(new CartCoupon(coupon.Code, coupon.Amount));
        }
    }
}