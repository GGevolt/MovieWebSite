using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Server.Model.AuthModels;

namespace MovieWebSite.Server.Data
{
    public class AuthDBContext : IdentityDbContext<ApplicationUser>
    {
        public AuthDBContext(DbContextOptions<AuthDBContext> option) : base(option)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().HasIndex(u=>u.UserName).IsUnique();
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "UserT0",
                    NormalizedName = "USERT0"
                },
                new IdentityRole
                {
                    Name = "UserT1",
                    NormalizedName = "USERT1"
                },
                new IdentityRole
                {
                    Name = "UserT2",
                    NormalizedName = "USERT2"
                },
            };
            foreach (var role in roles)
            {
                builder.Entity<IdentityRole>()
                    .HasData(role);
            }
        }
    }
}
