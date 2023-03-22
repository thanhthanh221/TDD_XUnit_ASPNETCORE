using CartTdd.Domain.CouponAggregate;
using CartTdd.Infrastructure.DataBase;
using MongoDB.Driver;

namespace CartTdd.Infrastructure.CouponAggregate
{
    public class CouponRepository : ICouponRepository
    {
        private readonly DbContext dbContext;

        public CouponRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Coupon> GetAsync(string code)
        {
            var filter = Builders<Coupon>.Filter.Eq(c => c.Code, code);

            var documents = await dbContext.Coupons.FindAsync(filter);
            var coupon = await documents.FirstOrDefaultAsync(); 

            return coupon;
        }
    }
}