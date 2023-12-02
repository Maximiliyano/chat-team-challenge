using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using ChatTeamChallenge.Contracts.Authentication;
using ChatTeamChallenge.Contracts.Enums;
using ChatTeamChallenge.Domain.Core.Errors;
using ChatTeamChallenge.Domain.Core.Primities.Result;
using ChatTeamChallenge.Domain.Helpers;
using ChatTeamChallenge.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ChatTeamChallenge.Infrastructure.JWT;

public sealed class JwtFactory : IJwtFactory
{
    private readonly JwtIssuerOptions _jwtOptions;
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
    private readonly IConfiguration _configuration;
    
    public JwtFactory(IOptions<JwtIssuerOptions> options, IConfiguration configuration)
    {
        _configuration = configuration;
        _jwtOptions = options.Value;
        ThrowIfInvalidOptions(_jwtOptions);

        _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
    }
    
    public async Task<AccessToken> GenerateAccessToken(int id, string userName, string email, CreativeRoles userRoles)
    {
        var identity = GenerateClaimsIdentity(id, userName, userRoles);
        
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userName),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
            new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
            identity.FindFirst("roles"),
            identity.FindFirst("id")
        };
        
        var token = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            _jwtOptions.NotBefore,
            _jwtOptions.Expiration,
            _jwtOptions.SigningCredentials);

        return new AccessToken(_jwtSecurityTokenHandler.WriteToken(token), (int)_jwtOptions.ValidFor.TotalSeconds);
    }

    private static ClaimsIdentity GenerateClaimsIdentity(int id, string userName, CreativeRoles roles)
    {
        return new ClaimsIdentity(new GenericIdentity(userName, "Token"), new[]
        {
            new Claim("id", id.ToString()),
            new Claim("roles", roles.ToString())
        });
    }
    
    public string GenerateRefreshToken()
    {
        return Convert.ToBase64String(SecurityHelper.GetRandomBytes());
    }

    public Result<int> GetUserIdFromToken(string accessToken)
    {
        var claimsPrincipal = GetPrincipalFromToken(accessToken, _configuration["JwtIssuerOptions:SecretKey"]!);

        return claimsPrincipal is null ? 
            Result.Failure<int>(DomainErrors.AccessToken.InvalidToken) : 
            Result.Success(int.Parse(claimsPrincipal.Claims.First(c => c.Type == "id").Value));
    }
    
    private static long ToUnixEpochDate(DateTime date)
    {
        return (long)Math.Round((date.ToUniversalTime() -
                                 new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
            .TotalSeconds);
    }
    
    private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
    {
        if (options is null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        if (options.ValidFor <= TimeSpan.Zero)
        {
            throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
        }

        if (options.SigningCredentials == null)
        {
            throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
        }

        if (options.JtiGenerator == null)
        {
            throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
        }
    }
    
    private ClaimsPrincipal GetPrincipalFromToken(string token, string signingKey) =>
        ValidateToken(token, new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey)),
            ValidateLifetime = false
        });
    
    private ClaimsPrincipal ValidateToken(string token, TokenValidationParameters tokenValidationParameters)
    {
        try
        {
            var principal = _jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
        catch (Exception)
        {
            return null!;
        }
    }
}