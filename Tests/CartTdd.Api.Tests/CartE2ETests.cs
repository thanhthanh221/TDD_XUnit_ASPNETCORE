using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace CartTdd.Api.Tests
{
    public class CartE2ETests : IClassFixture<CartE2ETestsFixture>
    {
        private readonly CartE2ETestsFixture fixture;

        public CartE2ETests(CartE2ETestsFixture fixture)
        {
            this.fixture = fixture;
        }

        public async Task Should_ReturnSuccess_When_ApplyCoupon()
        {
            var request = new
            {
                couponCode = "COUPON100"
            };

            var responseMessage = await fixture.httpClient.PostAsJsonAsync($"carts/{Guid.NewGuid()}/apply-coupon", request);
            
            Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);
        }
    }

}