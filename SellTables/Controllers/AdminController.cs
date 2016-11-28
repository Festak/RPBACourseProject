
using MultilingualSite.Filters;
using SellTables.Interfaces;
using SellTables.Models;
using SellTables.Services;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SellTables.Controllers
{
    [Authorize(Roles = "admin")]
    [Culture]
    public class AdminController : DefaultController
    {
        ApplicationDbContext DataBaseConnection;

        ICreativeService CreativeService;
        IUserService UserService;

        public AdminController(ApplicationDbContext DataBaseConnection)
        {
            this.DataBaseConnection = DataBaseConnection;
            CreativeService = new CreativeService(DataBaseConnection);
            UserService = new UserService(DataBaseConnection);
        }

        // for tests
        public AdminController(IUserService UserService, ICreativeService CreativeService)
        {
            this.UserService = UserService;
            this.CreativeService = CreativeService;
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

        [HttpPost]
        public void BanUser(string userName)
        {
            if (userName != null)
            {
                UserService.BanUser(userName);
            }
        }

        [HttpPost]
        public void UnbanUser(string userName)
        {
            if (userName != null)
            {
                UserService.UnbanUser(userName);
            }
        }

        private ICollection<ApplicationUser> GetUsers()
        {
            return UserService.GetAllUsers();
        }

        [HttpGet]
        public ActionResult UserPage(string username)
        {
            return View(UserService.GetUserByName(username));
        }

    }
}