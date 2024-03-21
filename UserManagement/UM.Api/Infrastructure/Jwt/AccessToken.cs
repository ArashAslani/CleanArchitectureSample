﻿using Common.Application.SecurityUtilities;
using System.IdentityModel.Tokens.Jwt;

namespace UM.Api.Infrastructure.Jwt;

public class AccessToken
{
    public string access_token { get; set; }
    public string refresh_token { get; set; }
    public string token_type { get; set; }
    public int expires_in { get; set; }

    public AccessToken(JwtSecurityToken securityToken)
    {
        access_token = new JwtSecurityTokenHandler().WriteToken(securityToken);
        token_type = "Bearer";
        expires_in = (int)(securityToken.ValidTo - DateTime.UtcNow).TotalSeconds;
        refresh_token = Guid.NewGuid().ToString();

    }
}


