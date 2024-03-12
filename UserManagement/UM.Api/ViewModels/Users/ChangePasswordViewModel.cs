using System.ComponentModel.DataAnnotations;

namespace UM.Api.ViewModels.Users;

public class ChangePasswordViewModel
{
    [Display(Name = "Current password")]
    [Required(ErrorMessage = "Please enter {0}")]
    public string CurrentPassword { get; set; }
    [Display(Name = "Current password")]
    [Required(ErrorMessage = "Please enter {0}")]
    [MinLength(6, ErrorMessage = "Password must be greater than 6 characters.")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Please enter your password.")]
    [MinLength(6, ErrorMessage = "Password must be greater than 6 characters.")]
    [Compare(nameof(Password), ErrorMessage = "Passwords are not the same.")]
    public string ConfirmPassword { get; set; }
}