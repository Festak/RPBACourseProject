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

    }
}