using Common.Domain.Exceptions;

namespace Common.Domain.ValueObjects;

public abstract class StronglyTypedId<T> : ValueObject
{
    private Guid _id;

    public Guid Value
    {
        get { return _id; }
        private set
        {
            if (value == Guid.Empty)
                throw new InvalidIdException("A valid id must be provided.");

            _id = value;
        }
    }

    protected StronglyTypedId(Guid value)
    {
        Value = value;
    }
}
