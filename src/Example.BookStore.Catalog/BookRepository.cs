namespace Example.BookStore.Catalog;

public class BookRepository
{
    private readonly CatalogContext _context;

    public BookRepository(CatalogContext context)
    {
        _context = context;
    }

    public async Task<Book?> Find(Guid id)
    {
        return await _context.Books.FindAsync(id);
    }

    public async Task<Book> GetById(Guid id)
    {
        var aggregate = await Find(id);
        if (aggregate == null)
            throw new EntityNotFound();
        return aggregate;
    }

    public Task Add(Book entity)
    {
        _context.Books.Add(entity);
        return Task.CompletedTask;
    }

    public Task Update(Book entity, int originalVersion)
    {
        var version = _context.Entry(entity).OriginalValues.GetValue<int>("Version");
        if ( version!= originalVersion)
            throw new OptimisticConcurrencyException(nameof(Book), version,originalVersion);
        _context.Books.Update(entity);
        return Task.CompletedTask;
    }
}

public class EntityNotFound : Exception
{
}