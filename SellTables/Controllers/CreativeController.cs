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
            CreativeService = DependencyResolver.Current.GetService<CreativeService>();
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
                CreativeService.AddCreative(creativemodel, db);
                return RedirectToAction("Index");
            }

            return View(creativemodel);
        }


        public void GetRatingFromView(int rating, CreativeViewModel creative) {
           CreativeService.SetRatingToCreative(rating, creative, db, FindUser());
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
            return View();
        }

        //private ICollection<CreativeViewModel> InitCreatives(ICollection<Creative> list)
        //{
        //    return list.Select(creative => new CreativeViewModel
        //    {
        //        Id = creative.Id,
        //        Chapters = InitChapters(creative.Chapters),
        //        UserName = creative.User.UserName,
        //        Name = creative.Name,
        //        Rating = creative.Rating,
        //        CreationDate = creative.CreationDate.ToShortDateString() + " " + creative.CreationDate.ToShortTimeString()
        //    }).ToList();
        //}


    }
}