namespace Example.BookStore.Catalog;

public abstract class DomainException : Exception
{
    protected DomainException(string message) : base(message)
    {
    }
}