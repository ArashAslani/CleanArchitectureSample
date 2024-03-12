using System.Text.RegularExpressions;

namespace Common.Domain.Utilities;

public static class StringHelper
{
    public static bool IsText(this string value)
    {
        var isNumber = Regex.IsMatch(value, @"^\d+$");
        return !isNumber;
    }
}