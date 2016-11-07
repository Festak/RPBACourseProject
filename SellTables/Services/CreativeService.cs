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
    public class CreativeService
    {
        private static IRepository<Creative> Repository;

        public CreativeService()
        {
            Repository = new CreativesRepository();
        }

        internal static List<Creative> GetAllCreatives()
        {
            var listOfUsers = Repository.GetAll();
            return listOfUsers.ToList();
        }

    }
}