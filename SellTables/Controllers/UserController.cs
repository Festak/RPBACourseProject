using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
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
    public class UserController : Controller
    {
        ApplicationDbContext dataBaseConnection = new ApplicationDbContext();
        CreativeService CreativeService;
        UserService UserService;

        public UserController()
        {
            CreativeService = new CreativeService(dataBaseConnection);
            UserService = new UserService(dataBaseConnection);
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

        public ActionResult Upload(byte[] img)
        {
            Account account = new Account(
                            "qwe123",
                            "361919682238885",
                            "rxw9_ETqk63uignEfF1R9TCcZ6I");
            Cloudinary cloudinary = new Cloudinary(account);
            if (img != null)
            {
                using (Stream str = new MemoryStream(img))
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription("name", str)
                    };
                    var uploadResult = cloudinary.Upload(uploadParams);
                    UserService.UpdateUserAvatar(uploadResult.SecureUri.ToString(), User.Identity.Name);
                }
            }
            return RedirectToAction("UserPage");
        }
    }
}