using Application.Interfaces;
using Domain.Models.Entities;
using Domain.Models.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Application.Services;

// сервис-поставщик jwt-токенов
public class JwtService : IJwtService
{
    // 1. создать токен безопасности
    public string CreateJwtToken(User user) {

        // ключ безопасности
        var secretKey = AuthOptions.GetSymmetricSecurityKey();

        // полномочия
        var signingCredentials =
            new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        // данные о пользователе для авторизации
        var claims = new List<Claim> {
            new("userName", user.UserName),
                new("phone", user.Phone),
                new("email", user.Email)
            };
        // добавление всех ролей
        user.Roles.ForEach(role => claims.Add(new Claim("role", role.Name)));

        // настройка токена
        var tokenOptions = new JwtSecurityToken(
            issuer:     AuthOptions.ISSUER,
            audience:   AuthOptions.AUDIENCE,
            claims:     claims,
            //expires:  DateTime.Now.AddMinutes(2),
            expires:    AuthOptions.TokenExpires(),
            signingCredentials: signingCredentials
        );

        // строка со значением токена безопасности
        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);

    } // CreateJwtToken


    // 2. создать токен обновления
    public string CreateUserToken() => Utils.GetRandomString();
        //$"{Guid.NewGuid().ToString().Substring(0, 8)}";

} // class JwtService