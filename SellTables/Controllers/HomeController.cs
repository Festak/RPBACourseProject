using MultilingualSite.Filters;
using SellTables.Lucene;
using SellTables.Models;
using SellTables.Services;
using System.Web.Mvc;

namespace SellTables.Controllers
{
    [Culture]
    public class HomeController : DefaultController
    {
        CreativeService CreativeService;
        UserService UserService;
        TagService TagService;
        ApplicationDbContext dataBaseConnection;

        public HomeController(ApplicationDbContext db)
        {
            dataBaseConnection = db;
            UserService = new UserService(dataBaseConnection);
            CreativeService = new CreativeService(dataBaseConnection);
            TagService = new TagService(dataBaseConnection); 
            
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

        public JsonResult GetLastEdited() {
            var lastEditedCreatives = CreativeService.GetLastEditedCreatives();
            return Json(lastEditedCreatives, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            CreativeSearch.AddUpdateLuceneIndex(CreativeService.GetAllCreativesForLucene());
            var allTags = TagService.GetMostPopularTags();
            return View(allTags);
        }

    }
}