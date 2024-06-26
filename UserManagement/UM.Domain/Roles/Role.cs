﻿using Common.Domain.Exceptions;
using Common.Domain;
using UM.Domain.Users.Enums;
using UM.Domain.Users.Services;
using UM.Domain.Users;

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
        var roleId = Guid.NewGuid().ToRoleIdInstance();
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
        var roleId = Guid.NewGuid().ToRoleIdInstance();
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
