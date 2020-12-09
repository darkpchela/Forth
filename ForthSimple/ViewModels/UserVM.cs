using System;

namespace ForthSimple.ViewModels
{
    public class UserVM
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public bool Blocked { get; set; }

        public DateTime RegisterDate { get; set; }

        public DateTime LastOnlineDate { get; set; }

        public bool Selected { get; set; }
    }
}