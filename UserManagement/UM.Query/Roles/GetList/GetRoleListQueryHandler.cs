using Common.Query;
using Microsoft.EntityFrameworkCore;
using UM.Infrastructure.Persistent.EFCore;
using UM.Query.Roles.DTOs;

namespace UM.Query.Roles.GetList;

public class GetRoleListQueryHandler : IQueryHandler<GetRoleListQuery, List<RoleDto>>
{
    private readonly UserManagmentContext _context;

    public GetRoleListQueryHandler(UserManagmentContext context)
    {
        _context = context;
    }
    public async Task<List<RoleDto>> Handle(GetRoleListQuery request, CancellationToken cancellationToken)
    {
        return await _context.Roles.Select(role => new RoleDto()
        {
            Id = role.Id,
            CreationDate = role.CreationDate,
            Permissions = role.Permissions.Select(s => s.Permission).ToList(),
            Title = role.Title
        }).ToListAsync(cancellationToken);
    }
}