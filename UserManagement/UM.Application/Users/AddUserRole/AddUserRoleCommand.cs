﻿using UM.Domain.Users;

namespace UM.Application.Users.AddUserRole
{
    public class AddUserRoleCommand : IBaseCommand
    {
        public UserId UserId { get; set; }
        public List<UserRole> UserRoles { get; set; }
    }
}
