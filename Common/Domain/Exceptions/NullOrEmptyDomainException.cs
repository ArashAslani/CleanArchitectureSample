namespace Common.Domain.Exceptions;

public class NullOrEmptyDomainDataException : BaseDomainException
{
    public NullOrEmptyDomainDataException()
    {

    }
    public NullOrEmptyDomainDataException(string message) : base(message)
    {

    }

    public static void CheckString(string value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new NullOrEmptyDomainDataException($"{fieldName} is null or empty.");
    }
}
