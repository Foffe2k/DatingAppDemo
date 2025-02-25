using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public class TokenService(IConfiguration config) : ITokenService
{
    public string CreateToken(AppUser user)
    {
        var tokenKey = config["TokenKey"] ?? throw new Exception("Cannot access tokeKey from appsettings"); // '??' = If null
        if (tokenKey.Length < 64) throw new Exception("Your tokenKey needs to be longer");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)); //SymmetricSecurityKey andvänder samma nyckel för att validera under hela processen. Asymmetric använder public/privat (som vid https t.ex.)

        var claims = new List<Claim> //Ett claim är ett påstående som leverars i en JWT token. Kan vara ett namn etc
        {
            new (ClaimTypes.NameIdentifier, user.UserName) //Eftersom vi redan veta att det är en Lista med Claims så kan vi förkorta 'new Claim(...)' till endast 'new (...)'
        };

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
