using System.ComponentModel.DataAnnotations;

namespace GuitarShop.Models;

public class RegistrationViewModel
{
    public long Id { get; set; }
    [Required]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }
    [Required]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }
    [Required]
    [Display(Name = "User Name")]
    public string UserName { get; set; }
    [Required]
    [Display(Name = "Password")]
    public string Password { get; set; }
    [Required]
    [Display(Name = "Confirm Password")]
    public string ConfirmPassword { get; set; }
    [Required]
    [Display(Name = "Email")]
    public string Email { get; set; }
}
