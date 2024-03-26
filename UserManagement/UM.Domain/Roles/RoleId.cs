using Common.Domain.Exceptions;
using Common.Domain.Utilities;
using Common.Domain.ValueObjects;

namespace UM.Domain.RoleAgg
{
    public class RoleId(Guid value) : StronglyTypedId<Guid>(value)
    {
    }

    public static class RoleIdUtilities
    {
        public static RoleId ToRoleIdInstance(this Guid value) => new(value);

        public static RoleId ToRoleIdInstance(this string value)
        {
            var (Status, Value) = value.IsGuid();

            if (Status)
                return Value.ToRoleIdInstance();
            else
                throw new InvalidIdException("Input String (for RoleId) is not type of GUID.");
        }
    }
}
