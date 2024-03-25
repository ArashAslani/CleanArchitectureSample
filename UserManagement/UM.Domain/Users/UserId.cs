using Common.Domain.ValueObjects;

namespace UM.Domain.Users
{
    public sealed class UserId(Guid value) : StronglyTypedId<UserId>(value)
    {
    }
}
