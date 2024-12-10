using System.IdentityModel.Tokens.Jwt;
using System.Text;
using JWTExample.API.Models;
using Microsoft.IdentityModel.Tokens;

namespace JWTExample.API.APIs;

public class LoginEndpoints
{
    private readonly IConfiguration _configuration;

    public LoginEndpoints(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    public Task<IResult> Login(UserLogin login)
    {
        if (login.Username == "admin" && login.Password == "admin")
        {
            var token = GenerateToken(login.Username);
            return Task.FromResult(Results.Ok(token));
        }

        return Task.FromResult(Results.Unauthorized());
    }

    private string GenerateToken(string loginUsername)
    {
        var secretKey = _configuration["JWT:Key"];
        var audienceToken = _configuration["JWT:Audience"];
        var issuerToken = _configuration["JWT:Issuer"];
        var newSymmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
        var credentials = new SigningCredentials(newSymmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuerToken,
            audience: audienceToken,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}