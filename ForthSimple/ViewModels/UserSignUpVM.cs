using System.ComponentModel.DataAnnotations;

namespace ForthSimple.ViewModels
{
    public class UserSignUpVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Password { get; set; }


        [Required]
        [StringLength(50, MinimumLength = 1)]
        [Compare("Password", ErrorMessage = "Passwords are different")]
        public string ConfirmPassword { get; set; }
    }
}