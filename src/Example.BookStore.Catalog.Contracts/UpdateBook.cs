using FluentValidation;
using MediatR;

namespace Example.BookStore.Catalog.Contracts;

public record UpdateBook : IRequest<Unit>
{
    public Guid Id { get; init; }
    public string? Title { get; init; }
    public int? Version { get; init; }
}

public class UpdateBookValidator:AbstractValidator<UpdateBook>{
    public UpdateBookValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Version).NotEmpty().GreaterThan(0);
    }
}