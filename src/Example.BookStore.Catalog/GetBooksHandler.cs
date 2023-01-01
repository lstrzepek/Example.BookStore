using System.Collections.Immutable;
using Example.BookStore.Catalog.Contracts;
using MediatR;

namespace Example.BookStore.Catalog;

public class GetBooksHandler : IRequestHandler<GetBooks, IReadOnlyCollection<BookListItem>>
{
    private readonly CatalogContext catalogContext;

    public GetBooksHandler(CatalogContext catalogContext)
    {
        this.catalogContext = catalogContext;
    }
    public Task<IReadOnlyCollection<BookListItem>> Handle(GetBooks request, CancellationToken cancellationToken)
    {
        var books = catalogContext.Books.Select(x => new BookListItem(
            x.Id,
            x.Title)).ToImmutableList();
        return Task.FromResult((IReadOnlyCollection<BookListItem>)books);
    }
}
