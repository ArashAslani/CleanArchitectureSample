using Common.Application.SecurityUtilities;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Common.Application.Validation;

public static class FluentValidations
{

    public static IRuleBuilderOptionsConditions<T, string> ValidPhoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder, string errorMessage = ValidationMessages.InvalidPhoneNumber)
    {
        return ruleBuilder.Custom((phoneNumber, context) =>
        {
            if (string.IsNullOrWhiteSpace(phoneNumber) || phoneNumber.Length is < 11 or > 11)
                context.AddFailure(errorMessage);

        });
    }
    public static IRuleBuilderOptionsConditions<T, TProperty> JustImageFile<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, string errorMessage = "You can only import the photo.") where TProperty : IFormFile?
    {
        return ruleBuilder.Custom((file, context) =>
        {
            if (file == null)
                return;

            if (!ImageValidator.IsImage(file))
            {
                context.AddFailure(errorMessage);
            }
        });
    }

}