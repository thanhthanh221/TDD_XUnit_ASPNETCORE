using CartTdd.Domain.CartAggregate;
using CartTdd.Domain.Exceptions;
using Xunit;

namespace CartTdd.Domain.Tests
{
    public class CouponApplierIntergrationTests : IClassFixture<CouponApplierIntergrationTestsFixture>
    {
        private readonly CouponApplierIntergrationTestsFixture fixture;
        private readonly Cart cart;

        public CouponApplierIntergrationTests(CouponApplierIntergrationTestsFixture fixture)
        {
            cart = new();
            this.fixture = fixture;
            cart.AddProduct(new CartProduct("SKU_1", 1, 100M));
            cart.AddProduct(new CartProduct("SKU_2", 2, 200M));
        }

        [Fact]
        public async Task Should_Succeed_When_ApplyCoupon()
        {
            await fixture.couponApplier.Apply("COUPON100",cart);

            Assert.Equal(400M, cart.TotalPrice);
            Assert.Equal("COUPON100", cart.Coupon.Code);
            Assert.Equal(100M, cart.Coupon.Amount);
        }

        [Fact]
        public async Task Should_ThrowException_When_ApplyCoupon_If_CouponIsNotFound()
        {
            var exception = await Assert.ThrowsAsync<CouponIsNotFoundException>(
                async () => await fixture.couponApplier.Apply("COUPON200", cart));

            Assert.Equal(500M, cart.TotalPrice);
            Assert.Null(cart.Coupon);
            Assert.Equal("Coupon is not found", exception.Message);
        }

        [Fact]
        public async Task Should_ThrowException_When_ApplyCoupon_If_CouponHasExpired()
        {
            var exception = await Assert.ThrowsAsync<CouponHasExpiredException>(
                async () => await fixture.couponApplier.Apply("EXPIRED_COUPON100", cart));

            Assert.Equal(500M, cart.TotalPrice);
            Assert.Null(cart.Coupon);
            Assert.Equal("Coupon has expired", exception.Message);

        }
    }
}