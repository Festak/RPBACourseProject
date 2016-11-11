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

        ApplicationDbContext db = new ApplicationDbContext();
        CreativeService CreativeService;
        UserService UserService;

        public AdminController()
        {
            CreativeService = new CreativeService(db);
            UserService = new UserService(db);
        }


        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }
    }
}