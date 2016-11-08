using MultilingualSite.Filters;
using Newtonsoft.Json;
using SellTables.Models;
using SellTables.Repositories;
using SellTables.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SellTables.Controllers
{
    [Culture]
    
    public class HomeController : Controller
    {
        CreativeService CreativeService;
        UserService UserService;
        TagService TagService;
        ApplicationDbContext db = new ApplicationDbContext();

        public HomeController() {

          UserService =  DependencyResolver.Current.GetService<UserService>();
          CreativeService =  DependencyResolver.Current.GetService<CreativeService>();
          TagService = DependencyResolver.Current.GetService<TagService>();
        }

        public ActionResult ChangeCulture(string lang)
        {
            string returnUrl = Request.UrlReferrer.AbsolutePath;
            List<string> cultures = new List<string>() { "ru", "en"};
            if (!cultures.Contains(lang))
            {
                lang = "ru";
            }
            HttpCookie cookie = Request.Cookies["lang"];
            if (cookie != null)
                cookie.Value = lang;  
            else
            {

                cookie = new HttpCookie("lang");
                cookie.HttpOnly = false;
                cookie.Value = lang;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return Redirect(returnUrl);
        }

        public JsonResult GetUsers() {
            var allUsers = UserService.GetAllUsers();
            return Json(allUsers, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCreatives()
        {
            var allCreatives = CreativeService.GetAllCreatives();
            return Json(allCreatives, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCreativesRange(int start, int count) {
            var rangeCreatives = CreativeService.GetCreativesRange(start, count, db);
            return Json(rangeCreatives, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTags() {
            var allTags = TagService.GetAllTags();
            return Json(allTags, JsonRequestBehavior.AllowGet);
        }

   
        public ActionResult Index()
        {
            return View();
        }

    }
}