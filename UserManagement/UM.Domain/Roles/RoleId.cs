using Common.Domain.ValueObjects;

namespace UM.Domain.RoleAgg
{
    public class RoleId(Guid value) : StronglyTypedId<Guid>(value)
    {
    }
}
