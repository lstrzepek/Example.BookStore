using FluentValidation;
using MediatR.Pipeline;

namespace Example.BookStore.Catalog;

public sealed class ValidationRequestPreProcessor<TRequest> : IRequestPreProcessor<TRequest>
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationRequestPreProcessor(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        if (!_validators.Any()) return Task.CompletedTask;
        var context = new ValidationContext<TRequest>(request);
        var failures = _validators
            .Select(x => x.Validate(context))
            .SelectMany(x => x.Errors)
            .Where(x => x != null)
            .GroupBy(
                x => x.PropertyName,
                x => x.ErrorMessage,
                (propertyName, errorMessages) => new
                {
                    Key = propertyName,
                    Values = errorMessages.Distinct().ToArray()
                })
            .ToDictionary(x => x.Key, x => x.Values);
        if (failures.Any()) throw new ValidationException(failures);

        return Task.CompletedTask;
    }
}