using System.Runtime.CompilerServices;
using CartTdd.Domain.CartAggregate;
using CartTdd.Domain.CouponAggregate;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

[assembly: InternalsVisibleTo("CartTdd.Domain.Tests")]
[assembly: InternalsVisibleTo("CartTdd.Api.Tests")]
namespace CartTdd.Infrastructure.DataBase;
public class DbContext
{
    public MongoClient Client { get; set; }
    public string DataBaseName { get; set; }

    public IMongoCollection<Coupon> Coupons { get; set; }
    public IMongoCollection<Cart> Carts { get; set; }

    public DbContext(IOptions<DbSettings> dbSettings)
    {
        Client = new MongoClient(dbSettings.Value.ConnectionString);
        var dataBase = Client.GetDatabase(dbSettings.Value.DataBase);

        Coupons = dataBase.GetCollection<Coupon>("coupons");
        Carts = dataBase.GetCollection<Cart>("carts");
    }
}