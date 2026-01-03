using Domyra.Application.Features.Validation;

namespace Domyra.Application.Exceptions;

public class DomyraValidationException : Exception
{
    private readonly ValidationResult _validationResult;

    public DomyraValidationException(ValidationResult result) : base()
    {
        _validationResult = result;
    }

    public ValidationResult GetValidationResult()
    {
        return _validationResult;
    }
}