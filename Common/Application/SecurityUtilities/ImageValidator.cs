using Microsoft.AspNetCore.Http;
using System.Drawing;

namespace Common.Application.SecurityUtilities;

public static class ImageValidator
{
    public static bool IsImage(this IFormFile? file)
    {
        if (file == null) return false;
        try
        {
            var img = Image.FromStream(file.OpenReadStream());
            return true;
        }
        catch
        {
            return false;
        }
    }
}
