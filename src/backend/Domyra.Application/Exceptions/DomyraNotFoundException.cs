namespace Domyra.Application.Exceptions;

public sealed class DomyraNotFoundException : Exception
{
    public DomyraNotFoundException(string name, object key)
        : base($"{name} with identifier '{key}' was not found.")
    {
    }
}
