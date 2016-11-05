using SellTables.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using SellTables.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SellTables.Repositories
{
    public class UsersRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UsersRepository()
        {
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        }


        async Task<bool> IUserRepository.CheckUserRole(string userId)
        {
            return await userManager.IsInRoleAsync(userId, "admin");
        }

        async Task<IdentityResult> IUserRepository.DeleteUser(ApplicationUser user)
        {
            return await userManager.DeleteAsync(user);
        }

        async Task<ApplicationUser> IUserRepository.FindUser(string userName)
        {
            return await userManager.FindByNameAsync(userName);
        }

       async Task<ApplicationUser> IUserRepository.FindUserById(string userId)
        {
           return await userManager.FindByIdAsync(userId);
        }

        ICollection<ApplicationUser> IUserRepository.GetAllUsers()
        {
            return userManager.Users.ToList();
        }

       async Task<IdentityResult> IUserRepository.UpdateUser(ApplicationUser user)
        {
            return await userManager.UpdateAsync(user);
        }
    }
}