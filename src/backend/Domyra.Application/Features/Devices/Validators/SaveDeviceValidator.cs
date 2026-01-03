using Domyra.Application.Contracts;
using Domyra.Application.Features.Devices.Commands;
using Domyra.Application.Features.Validation;

namespace Domyra.Application.Features.Devices.Validators;

public class SaveDeviceValidator : IRequestValidator<SaveDeviceCommand>
{
    public ValidationResult Validate(SaveDeviceCommand request)
    {
        var result = new ValidationResult();
        
        if (string.IsNullOrWhiteSpace(request.Name))   
            result.Add(nameof(request.Name), "Is required");
        
        switch (request.Name.Length)
        {
            case < 4:
                result.Add(nameof(request.Name), "Must be at least 4 characters long");
                break;
            case > 255:
                result.Add(nameof(request.Name), "Cannot be longer than 255 characters");
                break;
        }
        
        if (!Enum.IsDefined(request.State))
        {
            result.Add(nameof(request.State), "Invalid device state");
        }

        return result;
    }
}