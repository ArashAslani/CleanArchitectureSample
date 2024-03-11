namespace Common.Application.Validation;

public static class ValidationMessages
{
    public const string Required = "This field is required.";
    public const string InvalidPhoneNumber = "Invalid phone number.";
    public const string NotFound = "Requested information not found.";
    public const string MaxLength = "The entered characters exceed the allowed limit.";
    public const string MinLength = "The entered characters are fewer than the allowed limit.";

    public static string required(string field) => $"{field} is required.";
    public static string maxLength(string field, int maxLength) => $"{field} must be less than {maxLength} characters.";
    public static string minLength(string field, int minLength) => $"{field} must be more than {minLength} characters.";
}