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
    public class HomeController : DefaultController
    {
        CreativeService CreativeService;
        UserService UserService;
        TagService TagService;
        ApplicationDbContext dataBaseConnection = new ApplicationDbContext();

        public HomeController()
        {
            UserService = new UserService(dataBaseConnection);
            CreativeService = new CreativeService(dataBaseConnection);
            TagService = new TagService(dataBaseConnection);
        //    CreativeSearch.AddUpdateLuceneIndex(CreativeService.GetAllCreativesForLucene());
        }

 

        public JsonResult GetUsers()
        {
            var allUsers = UserService.GetAllUsers();
            return Json(allUsers, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCreatives()
        {
            var allCreatives = CreativeService.GetAllCreatives();
            return Json(allCreatives, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCreativesRange(int start, int count, int sortType)
        {
            var rangeCreatives = CreativeService.GetCreativesRange(start, count, sortType);
            return Json(rangeCreatives, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTags()
        {
            var allTags = TagService.GetAllModelTags();
            return Json(allTags, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPopular()
        {
            var popularCreatives = CreativeService.GetPopularCreatives();
            return Json(popularCreatives, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            var allTags = TagService.GetMostPopularTags();
            return View(allTags);
        }

    }
}