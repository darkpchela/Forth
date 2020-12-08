using System.ComponentModel.DataAnnotations;

namespace ForthSimple.ViewModels
{
    public class UserSignInVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Password { get; set; }
    }
}