using Microsoft.AspNet.Identity;
using MultilingualSite.Filters;
using SellTables.Models;
using SellTables.Services;
using SellTables.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SellTables.Controllers
{
    [Culture]
    [Authorize]
    public class CreativeController : Controller
    {
       ApplicationDbContext db = new ApplicationDbContext();

        CreativeService CreativeService;

        public CreativeController()
        {
            CreativeService = new CreativeService(db);
        }

        // GET: Creative
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home", new { area = "" });
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegisterCreativeModel creativemodel)
        {
            if (ModelState.IsValid)
            {
              creativemodel.Creative.User = FindUser();
                CreativeService.AddCreative(creativemodel);
                return RedirectToAction("Index");
            }

            return View(creativemodel);
        }

        public void GetRatingFromView(int rating, CreativeViewModel creative) {
           CreativeService.SetRatingToCreative(rating, creative, FindUser());
        }

        private ApplicationUser FindUser()
        {
            if (!System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                return null;
            return db.Users.Find(System.Web.HttpContext.Current.User.Identity.GetUserId());
        }

        public ActionResult Search(string query) {
            if (query == null) {
                return RedirectToAction("Index", "Home");
            }
            var list = Lucene.CreativeSearch.Search(query);
            var got = CreativeService.GetCreativesBySearch(list);
            return View();
        }

        public JsonResult GetCreativesByUser(string userName) {
            var creatives = CreativeService.GetCreativesByUser(userName);
            return Json(creatives, JsonRequestBehavior.AllowGet);
        }

    }
}