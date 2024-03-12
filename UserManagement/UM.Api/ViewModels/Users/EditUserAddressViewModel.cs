using System.ComponentModel.DataAnnotations;

namespace UM.Api.ViewModels.Users;

public class EditUserAddressViewModel
{
    public long Id { get; set; }
    [Required]
    public string Shire { get; set; }

    [Required]
    public string City { get; set; }

    [Required]
    public string PostalCode { get; set; }

    [Required]
    public string PostalAddress { get; set; }

    [Required]
    public string PhoneNumber { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string NationalCode { get; set; }
}