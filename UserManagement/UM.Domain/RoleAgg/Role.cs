using Common.Domain.Exceptions;
using Common.Domain;
using UM.Domain.UserAgg.Enums;
using UM.Domain.UserAgg.Services;
using UM.Domain.UserAgg;

namespace UM.Domain.RoleAgg;

public class Role : AggregateRoot<RoleId>
{
    public string Title { get; private set; }
    public List<RolePermission> Permissions { get; private set; }

    private Role()
    {
    }

    private Role(RoleId roleId, string title, List<RolePermission> permissions)
    {
        NullOrEmptyDomainDataException.CheckString(title, nameof(title));

        Id = roleId;
        Title = title;
        Permissions = permissions;
    }
    public static Role CreateNewWithPermissions(string title, List<RolePermission> permissions)
    {
        var roleId = new RoleId(Guid.NewGuid());
        return new Role(roleId, title, permissions);
    }


    private Role(RoleId roleId, string title)
    {
        NullOrEmptyDomainDataException.CheckString(title, nameof(title));
        Id = roleId;
        Title = title;
        Permissions = [];
    }

    public static Role CreateNew(string title)
    {
        var roleId = new RoleId(Guid.NewGuid());
        return new Role(roleId, title);
    }


    public void Edit(string title)
    {
        NullOrEmptyDomainDataException.CheckString(title, nameof(title));
        Title = title;
    }

    public void SetPermissions(List<RolePermission> permissions)
    {
        Permissions = permissions;
    }
}
