using Microsoft.AspNetCore.Identity;
using System;

namespace Forth.Identity.Entities
{
    public class User : IdentityUser<int>
    {
        public bool IsBlocked { get; set; }

        public DateTime RegisterDate { get; set; }

        public DateTime LastOnlineDate { get; set; }
    }
}