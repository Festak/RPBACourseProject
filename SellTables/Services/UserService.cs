using SellTables.Interfaces;
using SellTables.Models;
using SellTables.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SellTables.Services
{
    public class UserService
    {
        private IUserRepository Repository;
        private ApplicationDbContext db;
        public UserService(ApplicationDbContext db)
        {
            this.db = db;
            Repository = new UsersRepository(db);
        }

        internal List<ApplicationUser> GetAllUsers()
        {
            var listOfUsers = Repository.GetAllUsers();
            return listOfUsers.ToList();
        }

        public void AddCreativeToCounter(string userId) {
            ApplicationUser user = Repository.FindUserById(userId);
            user.ChaptersCreateCounter += 1;
            if (user.ChaptersCreateCounter == 5) // TODO: make verification for medal exist
            {
                    user.Medals.Add(db.Medals.FirstOrDefault(m => m.Id == 2));                
            }
            Repository.UpdateUser(user);
            db.SaveChanges();          
        }

        internal ApplicationUser GetCurrentUser(string name) {
            return Repository.GetCurrentUser(name);
        }
    }
}