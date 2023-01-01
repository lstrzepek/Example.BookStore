using MediatR;

namespace Example.BookStore.Catalog.Contracts;

public class GetBooks : IRequest<IReadOnlyCollection<BookListItem>>
{
}

public class BookListItem
{
    public BookListItem(Guid id, string title)
    {
        Id = id;
        Title = title;
    }

    public Guid Id { get; }
    public string Title { get; }
}