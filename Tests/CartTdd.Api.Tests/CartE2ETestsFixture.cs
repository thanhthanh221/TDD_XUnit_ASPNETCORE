using CartTdd.Domain.CartAggregate;
using CartTdd.Domain.CouponAggregate;
using CartTdd.Infrastructure.DataBase;
using Microsoft.Extensions.DependencyInjection;

namespace CartTdd.Api.Tests
{
    // Cung cấp tài nguyên
    public class CartE2ETestsFixture : IDisposable
    {
        private readonly CartApplication cartApplication;
        public HttpClient httpClient;
        public readonly Guid CartId = Guid.NewGuid();

        public CartE2ETestsFixture()
        {
            cartApplication = new();
            httpClient = cartApplication.CreateClient();

            SeedDataBase();
        }
        public void SeedDataBase()
        {
            using var scope = cartApplication.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<DbContext>();

            Cart cart = new(CartId);
            cart.AddProduct(new CartProduct("SKU_1", 1, 100M));
            cart.AddProduct(new CartProduct("SKU_2", 2, 200M));

            dbContext.Carts.InsertOne(cart);

            dbContext.Coupons.InsertOne(new Coupon("COUPON100", 100M, DateTime.Now.AddDays(1)));
            dbContext.Coupons.InsertOne(new Coupon("EXPIRED_COUPON100", 100M, DateTime.Now.AddDays(-1)));
        }

        public void DropDataBase()
        {
            using var scope = cartApplication.Services.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<DbContext>();
            dbContext.Client.DropDatabase("cart-tdd");
        }

        public void Dispose()
        {
            httpClient.Dispose();

            cartApplication.Dispose();
        }
    }
}