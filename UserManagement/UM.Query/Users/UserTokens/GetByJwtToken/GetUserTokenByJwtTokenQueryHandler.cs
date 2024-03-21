using Common.Query;
using Dapper;
using UM.Infrastructure.Persistant.Dapper;
using UM.Query.Users.DTOs;

namespace UM.Query.Users.UserTokens.GetByJwtToken;

internal class GetUserTokenByJwtTokenQueryHandler : IQueryHandler<GetUserTokenByJwtTokenQuery, UserTokenDto>
{
    private readonly DapperContext _dapperContext;

    public GetUserTokenByJwtTokenQueryHandler(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<UserTokenDto> Handle(GetUserTokenByJwtTokenQuery request, CancellationToken cancellationToken)
    {
        using var connection = _dapperContext.CreateConnection();
        var sql = $"SELECT TOP(1) " +
                     $"[Id],[UserId] As UserIdValue,[HashJwtToken],[HashRefreshToken],[TokenExpireDate],[RefreshTokenExpireDate],[Device],[CreationDate] " +
                     $"FROM {_dapperContext.UserTokens}" +
                     $" Where HashJwtToken=@hashJwtToken";
        return await connection.QueryFirstOrDefaultAsync<UserTokenDto>(sql, new { hashJwtToken = request.HashJwtToken });
    }
}