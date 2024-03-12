using Common.Query;
using UM.Query.Users.DTOs;

namespace UM.Query.Users.GetByFilter;

public class GetUserByFilterQuery : QueryFilter<UserFilterResult, UserFilterParams>
{
    public GetUserByFilterQuery(UserFilterParams filterParams) : base(filterParams)
    {
    }
}