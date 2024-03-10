using System.Reflection.Metadata.Ecma335;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public static class IdentityServiceExtensions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
    {
        //Adding authentication service so that our server have infromation to take a look at the token
        //and validate it based on the issuer signing key we have implemented.
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(option => {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    //Check the token signing key and make sure it is valid
                    ValidateIssuerSigningKey = true,
                    //Specify what our issuer signing key is
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding
                    .UTF8.GetBytes(config["TokenKey"])),
                    //We did not pass the Validate Issuer and Audience so we set them false
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });
        return services;
    }
}