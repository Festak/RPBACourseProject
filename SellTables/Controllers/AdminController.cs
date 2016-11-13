using SellTables.Models;
using SellTables.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SellTables.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        ApplicationDbContext DataBaseConnection = new ApplicationDbContext();

        CreativeService CreativeService;
        UserService UserService;

        public AdminController()
        {
            CreativeService = new CreativeService(DataBaseConnection);
            UserService = new UserService(DataBaseConnection);
        }


        public ActionResult Index()
        {
            return View(GetUsers());
        }

        [HttpPost]
        public ActionResult DeleteUser(string userName)
        {
            if (userName != null)
            {
                UserService.DeleteUser(userName);
            }
            return RedirectToAction("Index", "Admin");
        }

        private ICollection<ApplicationUser> GetUsers() {
            return UserService.GetAllUsers();
        }

    }
}