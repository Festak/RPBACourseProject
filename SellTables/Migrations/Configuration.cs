namespace SellTables.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SellTables.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SellTables.Models.ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var adminRole = new IdentityRole { Name = "admin" };
            var userRole = new IdentityRole { Name = "user" };
            roleManager.Create(adminRole);
            roleManager.Create(userRole);
          
            var admin = new ApplicationUser { Email = "admin@admin.com", UserName = "admin@admin.com" };
            if (context.Medals.FirstOrDefault(m => m.Id == 6) != null)
            {
                Medal medal = context.Medals.FirstOrDefault(m => m.Id == 6);
                admin.Medals.Add(medal);
            }
            string password = "!Q@w3e4";
            var result = userManager.Create(admin, password);
            if (result.Succeeded)
            {
                userManager.AddToRole(admin.Id, userRole.Name);
                userManager.AddToRole(admin.Id, adminRole.Name);
            }
            base.Seed(context);

        }
    }
}
