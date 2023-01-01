namespace Example.BookStore.Catalog;

public class ValidationException : DomainException
{
    public ValidationException(Dictionary<string, string[]> errors)
        : base("Validation Failure. One or more validation errors occurred")
    {
        Errors = errors;
    }

    public Dictionary<string, string[]> Errors { get; }
}