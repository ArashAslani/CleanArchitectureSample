using System.Security.Cryptography;
using System.Text;

namespace Common.Application.SecurityUtilities;

public static class PasswordHelper
{
    public static string EncodePasswordMd5(string pass) //Encrypt using MD5   
    {
        byte[] inputBytes = Encoding.ASCII.GetBytes(pass);
        byte[] hashBytes = MD5.HashData(inputBytes);

        // Convert the byte array to hexadecimal string
        StringBuilder sb = new();
        for (int i = 0; i < hashBytes.Length; i++)
        {
            sb.Append(hashBytes[i].ToString("X2"));
        }
        return sb.ToString();
    }
}