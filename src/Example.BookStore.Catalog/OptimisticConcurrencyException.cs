namespace Example.BookStore.Catalog;

public class OptimisticConcurrencyException : Exception
{
    public OptimisticConcurrencyException(string entityName, int expectedVersion, int actualVersion)
        :base($@"Invalid version received for {entityName}. Expected {expectedVersion} but was {actualVersion}")
    {
        
    }
}