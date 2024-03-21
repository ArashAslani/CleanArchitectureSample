using System.ComponentModel.DataAnnotations;
using Common.Application.Validation;

namespace UM.Api.ViewModels.Auth;

public class LoginViewModel
{
    public string? GrandType { get; set; } = "password";

    [Required(ErrorMessage = "Please enter your phone number.")]
    [MaxLength(11,ErrorMessage = ValidationMessages.InvalidPhoneNumber)]
    [MinLength(11, ErrorMessage = ValidationMessages.InvalidPhoneNumber)]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Please enter your password.")]
    public string Password { get; set; }

    public string? RefreshToken { get; set; }
    public string? Scope { get; set; }
    public string? ClientId { get; set; }
    public string? ClientSecret { get; set; }
}