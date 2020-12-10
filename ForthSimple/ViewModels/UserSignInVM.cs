using System.ComponentModel.DataAnnotations;

namespace ForthSimple.ViewModels
{
    public class UserSignInVM
    {
        [Required]
        public string Login { get; set; }


        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Password { get; set; }
    }
}