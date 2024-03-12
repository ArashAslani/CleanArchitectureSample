using System.ComponentModel.DataAnnotations;
using Common.Application.Validation.CustomValidation.IFormFile;
using UM.Domain.UserAgg.Enums;

namespace Shop.Api.ViewModels.Users;

public class EditUserViewModel
{
    [Display(Name = "Avatar")]
    [FileImage(ErrorMessage = "Profile avatar is invalid.")]
    public IFormFile? Avatar { get; set; }

    [Display(Name = "First name")]
    [Required(ErrorMessage = "Please enter your {0}")]
    public string FirstName { get; set; }

    [Display(Name = "Last name")]
    [Required(ErrorMessage = "Please enter your {0}")]
    public string LastName { get; set; }

    [Display(Name = "Phone number")]
    [Required(ErrorMessage = "Please enter your {0}")]
    public string PhoneNumber { get; set; }

    [Display(Name = "Email")]
    [Required(ErrorMessage = "Please enter your {0}")]
    public string Email { get; set; }

    public Gender Gender { get; set; } = Gender.None;
}