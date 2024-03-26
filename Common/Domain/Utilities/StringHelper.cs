using System.Text.RegularExpressions;

namespace Common.Domain.Utilities;

public static partial class StringHelper
{
    [GeneratedRegex(@"^\d+$")]
    private static partial Regex StringValidateRegax();

    public static bool IsText(this string value)
    {
        var isNumber = StringValidateRegax().IsMatch(value);
        return !isNumber;
    }

     public static (bool Status, Guid Value) IsGuid(this string value)
     {
        return (Guid.TryParse(value, out Guid GuidValue), GuidValue);
     }
}