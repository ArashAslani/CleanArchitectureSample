using Common.Query;
using Common.Query.Filter;
using UM.Domain.RoleAgg;
using UM.Domain.UserAgg;
using UM.Domain.UserAgg.Enums;

namespace UM.Query.Users.DTOs;

public class UserDto:BaseDto<UserId>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string AvatarName { get; set; }
    public bool IsActive { get; set; }
    public Gender Gender { get; set; }
    public List<UserRoleDto> Roles { get; set; }
}
public class UserRoleDto
{
    public RoleId RoleId { get; set; }
    public string RoleTitle { get; set; }
}


public class UserFilterData : BaseDto<UserId>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string AvatarName { get; set; }
    public Gender Gender { get; set; }
}

public class UserFilterParams:BaseFilterParam
{
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public UserId? Id { get; set; }
}
public class UserFilterResult : BaseFilter<UserFilterData,UserFilterParams, UserId>
{

}