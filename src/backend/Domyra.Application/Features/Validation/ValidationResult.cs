namespace Domyra.Application.Features.Validation;

public class ValidationResult
{
    private readonly List<ValidationError> _errors = new();

    public IReadOnlyList<ValidationError> Errors => _errors;
    
    public bool IsValid => _errors.Count == 0;

    public void Add(string field, string message)
        => _errors.Add(new ValidationError(field, message));

    public void AddRange(IEnumerable<ValidationError> errors)
        => _errors.AddRange(errors);

    public void Merge(ValidationResult? other)
    {
        if (other == null || other.IsValid)
            return;

        _errors.AddRange(other.Errors);
    }
}
