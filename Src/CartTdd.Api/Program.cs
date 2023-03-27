using CartTdd.Application.Cart;
using CartTdd.Domain.CartAggregate;
using CartTdd.Domain.CouponAggregate;
using CartTdd.Infrastructure.CartAggregate;
using CartTdd.Infrastructure.CouponAggregate;
using CartTdd.Infrastructure.DataBase;
using MediatR;


var builder = WebApplication.CreateBuilder(args);

DbConfiguration.Configure();
builder.Services.Configure<DbSettings>(builder.Configuration.GetRequiredSection(nameof(DbSettings)));
builder.Services.AddScoped<DbContext>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICouponRepository, CouponRepository>();
builder.Services.AddScoped<CouponApplier>();
builder.Services.AddMediatR(typeof(ApplyCoupon));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapPost("/carts/{id}/apply-coupon", async (Guid id, IMediator mediator, ApplyCoupon request) =>
{
    await mediator.Send(new ApplyCoupon(id, request.CouponCode));
});

app.MapGet("/carts/{id}", async (Guid id, IMediator mediator) => await mediator.Send(new GetCart(id)));

app.Run();