
using MultilingualSite.Filters;
using Microsoft.AspNet.Identity;
using SellTables.Models;
using SellTables.Services;
using System.Web.Mvc;

namespace SellTables.Controllers
{
    [Culture]
    [Authorize]
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

        public JsonResult GetCreativesByUser(string userName)
        {
            var creatives = CreativeService.GetCreativesByUser(userName);
            return Json(creatives, JsonRequestBehavior.AllowGet);
        }
        public bool IsCurrentUserIsAnAdmin()
        {
            var IsCurrentUserIsAnAdmin = UserService
                .IsCurrentUserIsAnAdmin(User.Identity.GetUserId());
            return IsCurrentUserIsAnAdmin;

        }

        public string GetUserAvatarUri(string userId) {
            string userAvatarUri = UserService.GetUserAvatarUri(userId);
            return userAvatarUri;
        }

        public void UploadUserAvatar(byte[] img)
        {
            CloudinaryService.UploadUserAvatar(img, User.Identity.Name);
            
        }
    }
}