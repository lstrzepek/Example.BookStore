using Example.BookStore.Catalog.Contracts;
using MediatR;

namespace Example.BookStore.Catalog;

public class RegisterBookHandler : IRequestHandler<RegisterBook>
{
    private readonly BookRepository _bookRepository;

    public RegisterBookHandler(BookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<Unit> Handle(RegisterBook request, CancellationToken cancellationToken)
    {
        await _bookRepository.Add(new Book(Guid.NewGuid(), request.Title, 1));
        return Unit.Value;
    }
}