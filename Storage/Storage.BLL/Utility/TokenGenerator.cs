using System.Security.Cryptography;

namespace Storage.BLL.Utility;

public static class TokenGenerator
{
    private const int DefaultBytesCount = 32;

    public static string GenerateToken(int bytesCount = DefaultBytesCount)
    {
        return Convert.ToBase64String(GetRandomBytes(bytesCount));
    }

    private static byte[] GetRandomBytes(int bytesCount)
    {
        var randomBytes = new byte[bytesCount];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return randomBytes;
    }
}