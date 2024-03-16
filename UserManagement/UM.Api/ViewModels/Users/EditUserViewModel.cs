using System.ComponentModel.DataAnnotations;
using Common.Application.Validation.CustomValidation.IFormFile;
using UM.Domain.UserAgg.Enums;

namespace UM.Api.ViewModels.Users;

public class EditUserViewModel
{
    [Display(Name = "Profile picture")]
    [FileImage(ErrorMessage = "Invalid {0}")]
    public IFormFile? Avatar { get; set; }

    [Display(Name = "First name")]
    [Required(ErrorMessage = "Enter {0}")]
    public string Name { get; set; }

    [Display(Name = "Last name")]
    [Required(ErrorMessage = "Enter {0}")]
    public string Family { get; set; }

    [Display(Name = "Phone number")]
    [Required(ErrorMessage = "Enter {0}")]
    public string PhoneNumber { get; set; }

    [Display(Name = "Email")]
    [Required(ErrorMessage = "Enter {0}")]
    public string Email { get; set; }

    public Gender Gender { get; set; } = Gender.None;
}