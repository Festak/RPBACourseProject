using MultilingualSite.Filters;
using Newtonsoft.Json;
using SellTables.Repositories;
using SellTables.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace SellTables.Controllers
{
    [Culture]
    public class HomeController : Controller
    {
        CreativeService creativeService;
        UserService userService;

        public HomeController() {

          userService =  DependencyResolver.Current.GetService<UserService>();
          creativeService =  DependencyResolver.Current.GetService<CreativeService>();
        }

        public ActionResult ChangeCulture(string lang)
        {
            string returnUrl = Request.UrlReferrer.AbsolutePath;
            // Список культур
            List<string> cultures = new List<string>() { "ru", "en"};
            if (!cultures.Contains(lang))
            {
                lang = "ru";
            }
            // Сохраняем выбранную культуру в куки
            HttpCookie cookie = Request.Cookies["lang"];
            if (cookie != null)
                cookie.Value = lang;   // если куки уже установлено, то обновляем значение
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
            var allUsers = userService.GetAllUsers();
            return Json(allUsers, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCreatives()
        {
            var allCreatives = creativeService.GetAllCreatives();
            return Json(allCreatives, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCreativesRange(int start, int count) {
            var rangeCreatives = CreativeService.GetCreativesRange(start, count);
            //   string jsonResult = JsonConvert.SerializeObject(allCreatives);
            return Json(rangeCreatives, JsonRequestBehavior.AllowGet);
        }


       // public IHttpActionResult GetAllCreatives()
       // {
       //      return Oz(CreativeService.GetAllCreatives());
       //  }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}