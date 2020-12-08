using ForthSimple.Etc;
using System.ComponentModel.DataAnnotations;

namespace ForthSimple.Models
{
    public class UserVM
    {
        public int? Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public StatusEnum Status { get; set; } = StatusEnum.Normal;
    }
}