namespace Spg.Payment.DomainModel.Exceptions 

public class ProductCreateException : Exception
{
    public ProductCreateException() { }

    public ProductCreateException(string message) : base(message) { }

    public ProductCreateException(string message, Exception innerException)
        : base(message, innerException) { }
}