using System.ComponentModel.DataAnnotations;
using Common.Application.Validation;

namespace UM.Api.ViewModels.Auth;

public class LoginViewModel
{
    [Required(ErrorMessage = "Please enter your phone number.")]
    [MaxLength(11,ErrorMessage = ValidationMessages.InvalidPhoneNumber)]
    [MinLength(11, ErrorMessage = ValidationMessages.InvalidPhoneNumber)]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Please enter your password.")]
    public string Password { get; set; }
}