using System.Security.Cryptography;
using System.Text;

namespace Common.Application.SecurityUtilities;

public class Sha256Hasher
{
    public static string Hash(string inputValue)
    {
        var originalBytes = Encoding.Default.GetBytes(inputValue);
        var encodedBytes = SHA256.HashData(originalBytes);
        return Convert.ToBase64String(encodedBytes);
    }
    public static bool IsCompare(string hashText, string rawText)
    {
        var hash = Hash(rawText);
        return hashText == hash;
    }
}