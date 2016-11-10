using MultilingualSite.Filters;
using Newtonsoft.Json;
using SellTables.Lucene;
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
    [Authorize]
    public class HomeController : Controller
    {
        CreativeService CreativeService;
        UserService UserService;
        TagService TagService;
        ApplicationDbContext db = new ApplicationDbContext();

        public HomeController() {
            UserService = new UserService(db);
            CreativeService = new CreativeService(db);
            TagService = new TagService(db);
         //CreativeSearch.AddUpdateLuceneIndex(CreativeService.GetAllCreativesForLucene());
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
       
        public JsonResult GetCreativesRange(int start, int count, int sortType) {
            var rangeCreatives = CreativeService.GetCreativesRange(start, count, sortType);
            return Json(rangeCreatives, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTags() {
           var allTags = TagService.GetAllModelTags();
          //  var allTags = TagService.GetMostPopularTags(); popular tags
            return Json(allTags, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPopular() {
            var popularCreatives = CreativeService.GetPopularCreatives();
            return Json(popularCreatives, JsonRequestBehavior.AllowGet);

        }

   
        public ActionResult Index()
        {
            return View();
        }

    }
}