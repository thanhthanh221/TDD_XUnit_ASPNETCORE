using Xunit;

namespace CartTdd.Api.Tests
{
    // Cung cấp tài nguyên
    public class CartE2ETestsFixture : IDisposable
    {
        private readonly CartApplication cartApplication;
        public readonly HttpClient httpClient;

        public CartE2ETestsFixture()
        {
            cartApplication = new();
            httpClient = cartApplication.CreateClient();
        }

        public void Dispose()
        {
            httpClient.Dispose();
            cartApplication.Dispose();
        }

        [Fact]
        public void Test1()
        {
            Assert.True(true);
        }
    }
}