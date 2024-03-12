using Common.Query;
using Microsoft.EntityFrameworkCore;
using UM.Infrastructure.Persistent.EFCore;
using UM.Query.Users.DTOs;

namespace UM.Query.Users.GetById;

public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserDto?>
{
    private readonly UserManagementContext _context;

    public GetUserByIdQueryHandler(UserManagementContext context)
    {
        _context = context;
    }

    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(f => f.Id == request.UserId, cancellationToken);
        if (user == null)
            return null;


        return await user.Map().SetUserRoleTitles(_context);
    }
}