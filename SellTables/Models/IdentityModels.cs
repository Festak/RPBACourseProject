using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System;

namespace SellTables.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Creative> Creatives { get; set; }
        public ICollection<Medal> Medals { get; set; }
        public int ChaptersCreateCounter { get; set; }
        public string AvatarUri { get; set; }
        public string Language { get; set; }
        public string Theme { get; set; }
        public DateTime RegistrationDate { get; set; }

        public ApplicationUser() {
            Creatives = new List<Creative>();
            Medals = new List<Medal>();
            Language = "en";
            Theme = "black";
            RegistrationDate = DateTime.Now;
            AvatarUri = "http://res.cloudinary.com/dum4mjc9q/image/upload/v1462886192/UserAvatars/defaultUser.png";
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public DbSet<Medal> Medals { get; set; }
        public DbSet<Creative> Creatives { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}