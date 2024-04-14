using HefestusApi.Models.Administracao;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class TokenService
{
    private readonly IConfiguration _config;

    public TokenService(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateToken(User user)
    {
        var claims = new[]
        {
            new Claim("userName", user.Name),
            user.Person?.UrlImage != null ? new Claim("UrlImage", user.Person.UrlImage) : new Claim("UrlImage", "https://avatars.githubusercontent.com/u/35440139?v=4"),
            new Claim("id", user.Id.ToString()),
            user.SystemLocationId != null ? new Claim("defaltLocationId", user.SystemLocationId.ToString()) : new Claim("defaltLocationId", null),
            user.DefaultLocation?.Person.Name != null? new Claim("defaultLocarionName", user.DefaultLocation.Person.Name.ToString()) : new Claim("defaultLocarionName", null),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWTSettings:key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(_config["JWTSettings:Issuer"],
            _config["JWTSettings:Audience"],
            claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public Dictionary<string, string> ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = GetValidationParameters();

        SecurityToken validatedToken;
        try
        {
            var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
            var claims = principal.Claims;

            var claimsDictionary = claims
                .GroupBy(c => c.Type)
                .ToDictionary(g => g.Key, g => g.Select(c => c.Value).FirstOrDefault());

            return claimsDictionary;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    private TokenValidationParameters GetValidationParameters()
    {
        return new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWTSettings:key"])),

            ValidateIssuer = true,
            ValidIssuer = _config["JWTSettings:Issuer"],

            ValidateAudience = true,
            ValidAudience = _config["JWTSettings:Audience"],

            ValidateLifetime = true,
        };
    }
}