using System.Diagnostics;
using Example.BookStore.Catalog.Contracts;
using MediatR;

namespace Example.BookStore.Catalog;

public class UpdateBookHandler:IRequestHandler<UpdateBook,Unit>
{
    private readonly BookRepository _bookRepository;

    public UpdateBookHandler(BookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }
    public async Task<Unit> Handle(UpdateBook request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetById(request.Id);
        book.Update(request);
        Debug.Assert(request.Version != null, "request.Version != null");
           await _bookRepository.Update(book, request.Version.Value);
        return Unit.Value;
    }
}