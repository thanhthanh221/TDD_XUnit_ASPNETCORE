using CartTdd.Domain.CartAggregate;
using MediatR;

namespace CartTdd.Application.Cart;

public class GetCartHandler : IRequestHandler<GetCart, GetCartResponse>
{
    private readonly ICartRepository cartRepository;

    public GetCartHandler(ICartRepository cartRepository)
    {
        this.cartRepository = cartRepository;
    }

    public async Task<GetCartResponse> Handle(GetCart request, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetAsync(request.Id);
        return new GetCartResponse
        {
            Id = cart.Id,
            TotalPrice = cart.TotalPrice,
            Products = cart.Products.Select(p => new GetCartProductResponse
            {
                Sku = p.Name,
                Quantity = p.Quantity,
                Price = p.Price
            }).ToList(),
            Coupon = cart.Coupon != null ? new GetCartCouponResponse
            {
                Code = cart.Coupon.Code,
                Amount = cart.Coupon.Amount
            } : null
        };
    }
}
