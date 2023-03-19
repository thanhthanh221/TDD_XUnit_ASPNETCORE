namespace CartTdd.Domain.Exceptions
{
    public class CartProductIsNotFoundException : Exception
    {
        public CartProductIsNotFoundException() : base("Product is not found")
        {
        }

        public CartProductIsNotFoundException(string message) : base(message)
        {
        }
    }
}