using System.Net;
using System.Net.Http.Json;
using CartTdd.Application.Cart;
using Xunit;

namespace CartTdd.Api.Tests;

public class CartE2ETests : IClassFixture<CartE2ETestsFixture>
{
    private readonly CartE2ETestsFixture fixture;

    public CartE2ETests(CartE2ETestsFixture fixture)
    {
        this.fixture = fixture;
    }
    [Fact]
    public async Task Should_ReturnSuccess_When_ApplyCoupon()
    {
        var request = new
        {
            couponCode = "COUPON100"
        };

        var responseMessage = await fixture.httpClient.PostAsJsonAsync($"carts/{fixture.CartId}/apply-coupon", request);

        Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);
    }

    [Fact]
    public async Task Should_ReturnCart_When_GetCart()
    {
        var responseMessage = await fixture.httpClient.GetFromJsonAsync<GetCartResponse>($"carts/{fixture.CartId}");
    
        Assert.NotNull(responseMessage.Coupon);
        Assert.Equal("COUPON100", responseMessage?.Coupon.Code);
        Assert.Equal(100M, responseMessage?.Coupon.Amount);
    }
}

