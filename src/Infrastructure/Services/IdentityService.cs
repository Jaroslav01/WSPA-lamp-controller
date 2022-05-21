using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CleanArchitecture.Infrastructure.Services;

public class IdentityService: IIdentityService
{
    private readonly IConfiguration Configuration;
    public IdentityService(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    private ClaimsIdentity GetIdentity(ApplicationUser user)
    {
        if (user != null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                //new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role),
                new Claim("Id", user.Id.ToString())
            };
            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
        // if user not found
        return null;
    }
    private JwtSecurityToken CreateJwtToken(ClaimsIdentity identity)
    {
        var jwt = new JwtSecurityToken(
                issuer: Configuration["Identity:ValidIssuer"],
                audience: Configuration["Identity:ValidAudience"],
                notBefore: DateTime.Now,
                claims: identity.Claims,
                //expires: expires,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(Configuration["Identity:IssuerSigningKey"])
                    ),
                    SecurityAlgorithms.HmacSha256
                )
        );
        return jwt;
    }
    private string EncodeJwtToken(JwtSecurityToken jwt)
    {
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
        return encodedJwt;
    }

    public string GenerateJwtToken(ApplicationUser user)
    {
        var claimsIdentity = this.GetIdentity(user);
        var jwtSecurityToken = this.CreateJwtToken(claimsIdentity);
        var jwtEncoded = this.EncodeJwtToken(jwtSecurityToken);
        return jwtEncoded;
    }
}