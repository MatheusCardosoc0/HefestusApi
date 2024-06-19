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

    public string GenerateTokenUser(User user)
    {
        var claims = new List<Claim>
    {
        new Claim("userName", user.Name),
        new Claim("UrlImage", user.Person?.UrlImage ?? "https://avatars.githubusercontent.com/u/35440139?v=4"),
        new Claim("systemLocationId", user.SystemLocationId.ToString()),
        new Claim("scope", "scope1")
    };

        // Adicionar apenas se não for nulo

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWTSettings:key1"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(_config["JWTSettings:Issuer"],
            _config["JWTSettings:Audience"],
            claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateTokenSystemLocation(SystemLocation systemLocation)
    {
        var claims = new List<Claim>
    {
        new Claim("Name", systemLocation.Name),
        new Claim("id", systemLocation.Id.ToString()),
        new Claim("scope", "scope3") // Adicionando a claim de escopo
    };

        // Adicionar apenas se não for nulo

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWTSettings:key3"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(_config["JWTSettings:Issuer"],
            _config["JWTSettings:Audience"],
            claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public Dictionary<string, string> ValidateTokenUser(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = GetValidationParametersUser();

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

    public Dictionary<string, string> ValidateTokenSystemLocation(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = GetValidationParametersSystemLocation();

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

    private TokenValidationParameters GetValidationParametersUser()
    {
        return new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWTSettings:key1"])),

            ValidateIssuer = true,
            ValidIssuer = _config["JWTSettings:Issuer"],

            ValidateAudience = true,
            ValidAudience = _config["JWTSettings:Audience"],

            ValidateLifetime = true,
        };
    }

    private TokenValidationParameters GetValidationParametersSystemLocation()
    {
        return new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWTSettings:key3"])),

            ValidateIssuer = true,
            ValidIssuer = _config["JWTSettings:Issuer"],

            ValidateAudience = true,
            ValidAudience = _config["JWTSettings:Audience"],

            ValidateLifetime = true,
        };
    }
}