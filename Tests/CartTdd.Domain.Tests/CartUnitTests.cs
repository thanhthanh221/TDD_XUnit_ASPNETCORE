using CartTdd.Domain.CartAggregate;
using CartTdd.Domain.Exceptions;
using Xunit;

namespace CartTdd.Domain.Tests
{
    public class CartUnitTests
    {
        private readonly Cart cart;

        public CartUnitTests()
        {
            cart = new Cart();
        }

        [Fact]
        public void Should_BeEmptyCart_When_CreateCart()
        {
            Assert.Equal(0,cart.TotalPrice);
            Assert.Empty(cart.Products);
        }

        [Fact]
        public void Should_Succeed_When_AddProduct()
        {
            cart.AddProduct(new CartProduct("SKU_1", 1, 100M));
            cart.AddProduct(new CartProduct("SKU_2", 2, 200M));

            Assert.Equal(2, cart.Products.Count);
            Assert.Equal(500M, cart.TotalPrice);
            
            var product1 = cart.Products.ElementAt(0);

            Assert.Equal("SKU_1", product1.Name);
            Assert.Equal(1, product1.Quantity);
            Assert.Equal(100M,product1.Price);

            var product2 = cart.Products.ElementAt(1);

            Assert.Equal("SKU_2", product2.Name);
            Assert.Equal(2, product2.Quantity);
            Assert.Equal(200M,product2.Price);
        }

        [Fact]
        public void Should_Succeed_When_RemoveProduct() {
            cart.AddProduct(new CartProduct("SKU_1", 1, 100M));
            cart.AddProduct(new CartProduct("SKU_2", 2, 200M));

            cart.RemoveProduct("SKU_2");

            Assert.Equal(100, cart.TotalPrice);
            Assert.Single(cart.Products);
        }

        [Fact]
        public void Should_ThrowException_When_RemoveProduct_If_ProductIsNotFound() {
            cart.AddProduct(new CartProduct("SKU_1", 1, 100M));
            cart.AddProduct(new CartProduct("SKU_2", 2, 200M));

            var exception = Assert.Throws<CartProductIsNotFoundException>(() => cart.RemoveProduct("SKU_3"));
            
            Assert.Equal("Product is not found", exception.Message);
        }
    }
}