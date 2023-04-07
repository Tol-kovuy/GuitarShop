using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GuitarShop.Models;

public class RegistrationViewModel
{
    [Required]
    [Display(Name = "First Name")]
    [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Only letters are allowed")]
    [StringLength(10, MinimumLength = 3, ErrorMessage = "The {0} must be at most {1} characters long.")]
    public string FirstName { get; set; }
    [Required]
    [Display(Name = "Last Name")]
    [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Only letters are allowed")]
    [StringLength(10, MinimumLength = 3, ErrorMessage = "The {0} must be at most {1} characters long.")]
    public string LastName { get; set; }
    [Required]
    [Display(Name = "User Name")]
    [StringLength(10, MinimumLength = 3, ErrorMessage = "The {0} must be at most {1} characters long.")]
    public string UserName { get; set; }
    [Required]
    [Display(Name = "Password")]
    [PasswordPropertyText(true)]
    [StringLength(10, MinimumLength = 6, ErrorMessage = "The {0} must be at most {1} characters long.")]
    public string Password { get; set; }
    [Required]
    [Display(Name = "Confirm Password")]
    [PasswordPropertyText(true)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
    [Required]
    [Display(Name = "Email")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }
}
