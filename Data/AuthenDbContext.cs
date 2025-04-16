using KetNoiDB.Models.Domain;
using KetNoiDB.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Reflection.Emit;
namespace KetNoiDB.Data
{
    public class AuthenDbContext:IdentityDbContext<User>
    {
        public AuthenDbContext(DbContextOptions<AuthenDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SeedRoles(modelBuilder);

        }

        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                 new IdentityRole
                 {
                     Name = "Admin",
                     ConcurrencyStamp = "1",
                     NormalizedName = "ADMIN"
                 },
                 new IdentityRole
                 {
                     Name = "User",
                     ConcurrencyStamp = "2",
                     NormalizedName = "USER"
                 },
                 new IdentityRole
                 {
                     Name = "Editor",
                     ConcurrencyStamp = "3",
                     NormalizedName = "EDITOR"
                 }
             );
        }
    }
}
