using System.ComponentModel.DataAnnotations;

namespace GuitarShop.Models
{
    public class AuthenticationViewModel
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set;}
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set;}
    }
}
