using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions;

public static class IdentityServiceExtensions
{
    public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration config)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            var tokenKey = config["TokenKey"] ?? throw new Exception("Token not found");
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
                ValidateIssuer = false, //Dubbelkollar att token är giltig genom att kontakta utgivaren (Issuer) av token, t.ex. via ett anropa till extern domän
                ValidateAudience = false //Dubbelkollar att användaren (Audience) är den användare som ska använda token, t.ex. att det är rätt api som använder der
            };
        });

        return services;
    }
}