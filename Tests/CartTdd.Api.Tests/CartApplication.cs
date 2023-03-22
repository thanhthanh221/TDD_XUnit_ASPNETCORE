using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;

namespace CartTdd.Api.Tests
{
    public class CartApplication : WebApplicationFactory<Program>
    {
        [Fact]
        public void Test1()
        {
            Assert.True(true);
        }
    }
}