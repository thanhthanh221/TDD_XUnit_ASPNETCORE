using CartTdd.Domain.CartAggregate;
using CartTdd.Domain.CouponAggregate;
using MediatR;

namespace CartTdd.Application.Cart;
public class ApplyCouponHandler : IRequestHandler<ApplyCoupon, ApplyCouponResponse>
{
    private readonly ICartRepository cartRepository;
    private readonly CouponApplier couponApplier;

    public ApplyCouponHandler(ICartRepository cartRepository, CouponApplier couponApplier)
    {
        this.cartRepository = cartRepository;
        this.couponApplier = couponApplier;
    }

    public async Task<ApplyCouponResponse> Handle(ApplyCoupon request, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetAsync(request.CartId);
        await couponApplier.Apply(request.CouponCode, cart);

        await cartRepository.UpdateAsync(cart);

        return new ApplyCouponResponse();
    }
}
