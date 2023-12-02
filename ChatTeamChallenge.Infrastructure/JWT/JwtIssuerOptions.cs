using Microsoft.IdentityModel.Tokens;

namespace ChatTeamChallenge.Infrastructure.JWT;

public class JwtIssuerOptions
{
  public required string Issuer { get; set; }

  public required string Subject { get; set; }

  public required string Audience { get; set; }

  public DateTime Expiration => IssuedAt.Add(ValidFor);

  public DateTime NotBefore => DateTime.UtcNow;

  public DateTime IssuedAt => DateTime.UtcNow;

  public TimeSpan ValidFor { get; init; } = TimeSpan.FromMinutes(120);

  public Func<Task<string>> JtiGenerator => () => Task.FromResult(Guid.NewGuid().ToString());

  public required SigningCredentials SigningCredentials { get; set; }
}
