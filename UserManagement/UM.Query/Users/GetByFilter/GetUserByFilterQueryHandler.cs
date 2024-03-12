using Common.Query;
using Microsoft.EntityFrameworkCore;
using UM.Infrastructure.Persistent.EFCore;
using UM.Query.Users.DTOs;

namespace UM.Query.Users.GetByFilter;

public class GetUserByFilterQueryHandler : IQueryHandler<GetUserByFilterQuery, UserFilterResult>
{
    private readonly UserManagmentContext _context;

    public GetUserByFilterQueryHandler(UserManagmentContext context)
    {
        _context = context;
    }

    public async Task<UserFilterResult> Handle(GetUserByFilterQuery request, CancellationToken cancellationToken)
    {
        var @params = request.FilterParams;
        var result = _context.Users.OrderByDescending(d => d.Id).AsQueryable();

        if (!string.IsNullOrWhiteSpace(@params.Email))
            result = result.Where(r => r.Email.Contains(@params.Email));

        if (!string.IsNullOrWhiteSpace(@params.PhoneNumber))
            result = result.Where(r => r.PhoneNumber.Contains(@params.PhoneNumber));

        if (@params.Id != null)
            result = result.Where(r => r.Id == @params.Id);

        var skip = (@params.PageId - 1) * @params.Take;
        var model = new UserFilterResult()
        {
            Data = await result.Skip(skip).Take(@params.Take)
                .Select(user => user.MapFilterData()).ToListAsync(cancellationToken),
            FilterParams = @params
        };

        model.GeneratePaging(result, @params.Take, @params.PageId);
        return model;
    }
}