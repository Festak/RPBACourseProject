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

        bool IUserRepository.CheckUserRole(string userId)
        {
            return userManager.IsInRole(userId, "admin");
        }

       IdentityResult IUserRepository.DeleteUser(ApplicationUser user)
        {
            return userManager.Delete(user);
        }

       ApplicationUser IUserRepository.FindUser(string userName)
        {
            return userManager.FindByName(userName);
        }

       ApplicationUser IUserRepository.FindUserById(string userId)
        {
           return userManager.FindById(userId);
        }

        ICollection<ApplicationUser> IUserRepository.GetAllUsers()
        {
            return userManager.Users.ToList();
        }

     ApplicationUser IUserRepository.GetCurrentUser(string id)
        {
            return userManager.FindByName(id);
        }

        IdentityResult IUserRepository.UpdateUser(ApplicationUser user)
        {
            return userManager.Update(user);
        }
    }
}