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

        public DateTime RegistrationDate { get; set; }

        public ApplicationUser() {
            Creatives = new List<Creative>();
            Medals = new List<Medal>();
            RegistrationDate = DateTime.Now;
            AvatarUri = "https://res.cloudinary.com/festak/image/upload/v1479038549/defaultUser_fofp7w.png";
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

        public virtual DbSet<Chapter> Chapters { get; set; }
        public virtual DbSet<Rating> Rating { get; set; }
        public virtual DbSet<Medal> Medals { get; set; }
        public virtual DbSet<Creative> Creatives { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Subscribe> Subscribes { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}