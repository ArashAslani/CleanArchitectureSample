using System.ComponentModel.DataAnnotations;
using Common.Application.Validation;

namespace UM.Api.ViewModels.Auth;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Please enter your phone number.")]
    [MaxLength(11, ErrorMessage = ValidationMessages.InvalidPhoneNumber)]
    [MinLength(11, ErrorMessage = ValidationMessages.InvalidPhoneNumber)]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Please enter your password.")]
    [MinLength(6,ErrorMessage = "Password must be greater than 6 characters.")]
    public string Password { get; set; }


    [Required(ErrorMessage = "Please enter your password.")]
    [MinLength(6, ErrorMessage = "Password must be greater than 6 characters.")]
    [Compare(nameof(Password),ErrorMessage = "Passwords are not the same. ")]
    public string ConfirmPassword { get; set; }
}