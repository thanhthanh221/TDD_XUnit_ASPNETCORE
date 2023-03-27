using CartTdd.Domain.CartAggregate;
using CartTdd.Domain.CouponAggregate;
using CartTdd.Domain.Exceptions;
using Moq;
using Xunit;

namespace CartTdd.Domain.Tests
{
    public class CouponApplierTests
    {
        private readonly Cart cart;

        public CouponApplierTests()
        {
            cart = new(Guid.NewGuid());
            cart.AddProduct(new CartProduct("SKU_1", 1, 100M));
            cart.AddProduct(new CartProduct("SKU_2", 2, 200M));
        }

        [Fact]
        public async Task Should_Succeed_When_ApplyCoupon()
        {
            Coupon coupon = new("COUPON100", 100M, DateTime.Now.AddDays(1));
            Mock<ICouponRepository> mockCouponRepository = new();
            mockCouponRepository.Setup(s => s.GetAsync(It.IsAny<string>())).ReturnsAsync(coupon);

            CouponApplier couponApplier = new(mockCouponRepository.Object);
            await couponApplier.Apply("COUPON100", cart);

            Assert.Equal(400M, cart.TotalPrice);
            Assert.Equal("COUPON100", cart.Coupon.Code);
            Assert.Equal(100M, cart.Coupon.Amount);
        }

        [Fact]
        public async Task Should_ThrowException_When_ApplyCoupon_If_CouponIsNotFound()
        {
            Mock<ICouponRepository> mockCouponRepository = new();
            mockCouponRepository.Setup(s => s.GetAsync(It.IsAny<string>())).Returns(Task.FromResult<Coupon>(null));

            CouponApplier couponApplier = new(mockCouponRepository.Object);

            var exception = await Assert.ThrowsAsync<CouponIsNotFoundException>(
                async () => await couponApplier.Apply("COUPON200", cart));

            Assert.Equal(500M, cart.TotalPrice);
            Assert.Null(cart.Coupon);
            Assert.Equal("Coupon is not found", exception.Message);
        }

        [Fact]
        public async Task Should_ThrowException_When_ApplyCoupon_If_CouponHasExpired()
        {
            Coupon coupon = new("EXPIRED_COUPON100", 100M, DateTime.Now.AddDays(-1));

            Mock<ICouponRepository> mockCouponRepository = new();
            mockCouponRepository.Setup(s => s.GetAsync(It.IsAny<string>())).ReturnsAsync(coupon);

            CouponApplier couponApplier = new(mockCouponRepository.Object);
            var exception = await Assert.ThrowsAsync<CouponHasExpiredException>(
                async () => await couponApplier.Apply("EXPIRED_COUPON100", cart));

            Assert.Equal(500M, cart.TotalPrice);
            Assert.Null(cart.Coupon);
            Assert.Equal("Coupon has expired", exception.Message);

        }
    }
}