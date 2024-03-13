﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using UM.Query.Users.DTOs;

namespace UM.Api.Infrastructure.Jwt;

public class JwtTokenBuilder
{
    public static string BuildToken(UserDto user, IConfiguration configuration)
    {
        var roles = user.Roles.Select(s => s.RoleTitle);
        var claims = new List<Claim>()
        {
            new(ClaimTypes.MobilePhone,user.PhoneNumber),
            new(ClaimTypes.NameIdentifier,user.Id.ToString()),
            new(ClaimTypes.Role,string.Join("-",roles))
        };
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfig:SignInKey"]));
        var credential = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["JwtConfig:Issuer"],
            audience: configuration["JwtConfig:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(7),
            signingCredentials: credential);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }



}