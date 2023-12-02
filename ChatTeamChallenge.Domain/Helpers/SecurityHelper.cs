using System.Security.Cryptography;

namespace ChatTeamChallenge.Domain.Helpers;

public static class SecurityHelper
{
    public static byte[] GetRandomBytes(int length = 32)
    {
#pragma warning disable SYSLIB0023
        using var randomNumberGenerator = new RNGCryptoServiceProvider();
#pragma warning restore SYSLIB0023
        
        var salt = new byte[length];
        randomNumberGenerator.GetBytes(salt);

        return salt;
    }
}