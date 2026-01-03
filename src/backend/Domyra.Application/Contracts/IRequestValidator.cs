using Domyra.Application.Features.Validation;

namespace Domyra.Application.Contracts;

public interface IRequestValidator<in TRequest>
{
    ValidationResult Validate(TRequest request);
}