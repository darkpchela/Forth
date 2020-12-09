using System;
using System.ComponentModel.DataAnnotations;

namespace ForthSimple.Models
{
    public class User
    {
        [Key]
        public int? Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool Blocked { get; set; }

        public DateTime RegisterDate{ get; set; }

        public DateTime LastOnlineDate { get; set; }
    }
}