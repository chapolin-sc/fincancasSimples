using System.Text;
using financasSimples.Identity.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace financasSimples.Api.Extensions;

public static class AuthenticationSetup
{
    public static void AddAuthenticationSetup(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettingOptions = configuration.GetSection(nameof(JwtOptions));
        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetSection("JwtOptions:SecurityKey").Value));

        services.Configure<JwtOptions>(options => {
            options.Issuer = jwtSettingOptions[nameof(JwtOptions.Issuer)];
            options.Audience = jwtSettingOptions[nameof(JwtOptions.Audience)];
            options.SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
            options.Expiration = int.Parse(jwtSettingOptions[nameof(JwtOptions.Expiration)] ?? "0");
        });

        services.Configure<IdentityOptions>(options => {

            options.Password.RequireDigit = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = false;
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 6;

        });

        var tokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidIssuer = configuration.GetSection("JwtOptions:Issuer").Value,

            ValidateAudience = true,
            ValidAudience = configuration.GetSection("JwtOptions:Audience").Value,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = securityKey,

            RequireExpirationTime = true,
            ValidateLifetime = true,

            ClockSkew = TimeSpan.Zero
        };

        services.AddAuthentication(options => 
        {
            
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        })
            .AddJwtBearer(options => {

                options.TokenValidationParameters = tokenValidationParameters;

            });
    }
}