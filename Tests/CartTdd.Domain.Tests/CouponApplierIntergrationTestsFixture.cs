using CartTdd.Domain.CouponAggregate;
using CartTdd.Infrastructure.CouponAggregate;
using CartTdd.Infrastructure.DataBase;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CartTdd.Domain.Tests
{
    public class CouponApplierIntergrationTestsFixture : IDisposable
    {
        private readonly DbContext dbContext;
        public readonly CouponApplier couponApplier;
        public CouponApplierIntergrationTestsFixture()
        {
            dbContext = new DbContext(Options.Create(new DbSettings
            {
                ConnectionString = "mongodb://localhost:27017",
                DataBase = $"cart-tdd"
            }));
            
            dbContext.Coupons.InsertOne(new Coupon("COUPON100", 100M, DateTime.Now.AddDays(1)));
            dbContext.Coupons.InsertOne(new Coupon("EXPIRED_COUPON100", 100M, DateTime.Now.AddDays(-1)));

            couponApplier = new CouponApplier(new CouponRepository(dbContext));
        }

        public void Dispose()
        {
            dbContext.Client.DropDatabase("cart-tdd");
        }
    }
}