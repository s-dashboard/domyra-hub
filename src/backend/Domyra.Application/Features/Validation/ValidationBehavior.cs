using Domyra.Application.Contracts;
using Domyra.Application.Exceptions;
using MediatR;

namespace Domyra.Application.Features.Validation;

public class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly IEnumerable<IRequestValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IRequestValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next(cancellationToken);

        var result = new ValidationResult();

        foreach (var validator in _validators)
            result.Merge(validator.Validate(request));

        if (!result.IsValid)
            throw new DomyraValidationException(result);

        return await next(cancellationToken);
    }
}


