using SellTables.Models;
using SellTables.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SellTables.Controllers
{
    public class AdminController : Controller
    {

        ApplicationDbContext dataBaseConnection = new ApplicationDbContext();

        CreativeService CreativeService;
        UserService UserService;

        public AdminController()
        {
            CreativeService = new CreativeService(dataBaseConnection);
            UserService = new UserService(dataBaseConnection);
        }


        public ActionResult Index()
        {
            return View(dataBaseConnection.Users.ToList());
        }
    }
}