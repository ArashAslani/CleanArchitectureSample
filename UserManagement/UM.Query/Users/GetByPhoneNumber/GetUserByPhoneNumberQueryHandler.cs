using Common.Query;
using Microsoft.EntityFrameworkCore;
using UM.Infrastructure.Persistent.EFCore;
using UM.Query.Users.DTOs;

namespace UM.Query.Users.GetByPhoneNumber;

public class GetUserByPhoneNumberQueryHandler : IQueryHandler<GetUserByPhoneNumberQuery, UserDto?>
{
    private readonly UserManagmentContext _context;

    public GetUserByPhoneNumberQueryHandler(UserManagmentContext context)
    {
        _context = context;
    }

    public async Task<UserDto?> Handle(GetUserByPhoneNumberQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(f => f.PhoneNumber == request.PhoneNumber, cancellationToken);

        if (user == null)
            return null;


        return await user.Map().SetUserRoleTitles(_context);
    }
}