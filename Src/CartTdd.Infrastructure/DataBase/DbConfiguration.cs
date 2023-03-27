using CartTdd.Domain.CartAggregate;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace CartTdd.Infrastructure.DataBase;

public static class DbConfiguration
{
    public static void Configure()
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));

        BsonClassMap.RegisterClassMap<Cart>(c => 
        {
            c.AutoMap();
            c.MapField("_products").SetElementName("Products");
        });
    }
}
