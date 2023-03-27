using MediatR;

namespace CartTdd.Application.Cart;

public record GetCart(Guid Id) : IRequest<GetCartResponse>;

public record GetCartResponse
{
    public Guid Id { get; set; }
    public decimal TotalPrice { get; set; }
    public List<GetCartProductResponse> Products { get; set; }
    public GetCartCouponResponse Coupon { get; set; }
}

public class GetCartCouponResponse
{
    public string Code { get; init; }
    public decimal Amount { get; init; }
}

public class GetCartProductResponse
{
    public string Sku { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

