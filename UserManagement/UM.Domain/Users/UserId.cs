using Common.Domain.Exceptions;
using Common.Domain.Utilities;
using Common.Domain.ValueObjects;

namespace UM.Domain.Users
{
    public sealed class UserId(Guid value) : StronglyTypedId<UserId>(value)
    {
    }

    public static class UserIdUtilities
    {
        public static UserId ToUserIdInstance(this Guid value) => new(value);
        public static UserId ToUserIdInstance(this string value)
        {
            var (Status, Value) = value.IsGuid();

            if (Status)
                return Value.ToUserIdInstance();
            else
                throw new InvalidIdException("Input String (for UserId) is not type of GUID.");
        }

    }
}
