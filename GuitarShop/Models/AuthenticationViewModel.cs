using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GuitarShop.Models
{
    public class AuthenticationViewModel
    {
        [Required]
        [Display(Name = "User Name")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string UserName { get; set;}
        [Required]
        [Display(Name = "Password")]
        [PasswordPropertyText(false)]
        [StringLength(10, MinimumLength = 5, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Password { get; set;}
    }
}
