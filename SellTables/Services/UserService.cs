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

        public UserService()
        {
            Repository = new UsersRepository();
        }

        internal List<ApplicationUser> GetAllUsers()
        {
            var listOfUsers = Repository.GetAllUsers();
            return listOfUsers.ToList();
        }

    }
}