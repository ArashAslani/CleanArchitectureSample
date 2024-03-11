using Common.Domain.ValueObjects;

namespace UM.Domain.UserAgg
{
    public sealed class UserId(Guid value) : StronglyTypedId<UserId>(value)
    {
    }
}
