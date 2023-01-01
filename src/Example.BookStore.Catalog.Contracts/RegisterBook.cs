using FluentValidation;
using MediatR;

namespace Example.BookStore.Catalog.Contracts;

public class RegisterBook : IRequest
{
    public RegisterBook(string title)
    {
        Title = title;
    }

    public string Title { get; }
}

public class RegisterBookValidator : AbstractValidator<RegisterBook>
{
    public RegisterBookValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
    }
}