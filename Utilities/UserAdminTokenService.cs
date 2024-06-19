using HefestusApi.Models.Administracao;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class UserAdminTokenService
{
    private readonly IConfiguration _config;

    public UserAdminTokenService(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateToken(UserAdmin userAdmin)
    {
        var claims = new List<Claim>
    {
        new Claim("id", userAdmin.Id),
        new Claim("scope", "scope2") // Adicionando a claim de escopo
    };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWTSettings:Key2"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _config["JWTSettings:Issuer"],
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
        catch (Exception)
        {
            return null;
        }
    }

    private TokenValidationParameters GetValidationParameters()
    {
        return new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWTSettings:key2"])),

            ValidateIssuer = true,
            ValidIssuer = _config["JWTSettings:Issuer"] ?? throw new ArgumentNullException("JWTSettings:Issuer"),

            ValidateAudience = true,
            ValidAudience = _config["JWTSettings:Audience"] ?? throw new ArgumentNullException("JWTSettings:Audience"),

            ValidateLifetime = true,
        };
    }
}
