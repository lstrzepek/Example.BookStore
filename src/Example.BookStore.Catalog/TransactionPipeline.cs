using MediatR;

namespace Example.BookStore.Catalog;

public class TransactionPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly CatalogContext _context;

    public TransactionPipeline(CatalogContext context)
    {
        _context = context;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken ct, RequestHandlerDelegate<TResponse> next)
    {
        await using var tx = await _context.Database.BeginTransactionAsync(ct);

        var result = await next();

        await _context.SaveChangesAsync(ct);
        await tx.CommitAsync(ct);

        return result;
    }
}