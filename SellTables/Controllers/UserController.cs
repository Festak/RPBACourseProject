using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MultilingualSite.Filters;
using Microsoft.AspNet.Identity;
using SellTables.Models;
using SellTables.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace SellTables.Controllers
{
    [Culture]
    public class UserController : DefaultController
    {
        ApplicationDbContext dataBaseConnection = new ApplicationDbContext();
        CreativeService CreativeService;
        UserService UserService;
        CloudinaryService CloudinaryService;

        public UserController()
        {
            CreativeService = new CreativeService(dataBaseConnection);
            UserService = new UserService(dataBaseConnection);
            CloudinaryService = new CloudinaryService(dataBaseConnection);
        }


        public ActionResult UserPage(string name)
        {
            if (name == null)
            {
                return View(UserService.GetCurrentUser(User.Identity.Name));
            }
            else
                return View(UserService.GetUserByName(name));
        }

        public ActionResult Settings()
        {
            return View(UserService.GetCurrentUser(User.Identity.Name));
        }

        public JsonResult GetCreativesByUser(string userName)
        {
            var creatives = CreativeService.GetCreativesByUser(userName);
            return Json(creatives, JsonRequestBehavior.AllowGet);
        }
        public bool IsCurrentUserIsAnAdmin()
        {
            var IsCurrentUserIsAnAdmin = UserService.IsCurrentUserIsAnAdmin(User.Identity.GetUserId());
            return IsCurrentUserIsAnAdmin;

        }

        public ActionResult UploadUserAvatar(byte[] img)
        {
            CloudinaryService.UploadUserAvatar(img, User.Identity.Name);
            return RedirectToAction("UserPage");
        }
    }
}