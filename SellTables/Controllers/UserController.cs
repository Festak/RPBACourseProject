
using MultilingualSite.Filters;
using Microsoft.AspNet.Identity;
using SellTables.Models;
using SellTables.Services;
using System.Web.Mvc;
using SellTables.Interfaces;

namespace SellTables.Controllers
{
    [Culture]
    [Authorize]
    public class UserController : DefaultController
    {
        ApplicationDbContext dataBaseConnection;
        CreativeService CreativeService;
        CategoryService CategoryService;
        IUserService UserService;
        ICloudinaryService CloudinaryService;

        public UserController(ApplicationDbContext DataBaseConnection)
        {
            this.dataBaseConnection = DataBaseConnection;
            CreativeService = new CreativeService(dataBaseConnection);
            UserService = new UserService(dataBaseConnection);
            CloudinaryService = new CloudinaryService(dataBaseConnection);
            CategoryService = new CategoryService(dataBaseConnection);
        }

        //for tests
        //public UserController(ICreativeService CreativeService, IUserService UserService, ICloudinaryService CloudinaryService) {
        //    this.CreativeService = CreativeService;
        //    this.UserService = UserService;
        //    this.CloudinaryService = CloudinaryService;
        //}


        public ActionResult UserPage(string name)
        {
            ViewBag.Creatives = CreativeService.GetCreativesBySubscribe(User.Identity.GetUserId());
            if (name == null)
            {
                return View(UserService.GetCurrentUser(User.Identity.Name));
            }
            else
            {
           
                return View(UserService.GetUserByName(name));
            }
        }

        public ActionResult SubscribeCategory() {
            return View(CategoryService.GetCategoriesWithSbs(User.Identity.GetUserId()));
        }

        public ActionResult Subscribe(int id) {
            CategoryService.SubscribeToCategory(id, User.Identity.GetUserId());
            return RedirectToAction("SubscribeCategory", "User");
        }

        public ActionResult UnSubscribe(int id) {
            CategoryService.UnSubscribeFromCategory(id, User.Identity.GetUserId());
            return RedirectToAction("SubscribeCategory", "User");
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