using Forth.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ForthSimple.Identity
{
    public class IdentityContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public IdentityContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}