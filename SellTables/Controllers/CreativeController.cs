using Microsoft.AspNet.Identity;
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

        //// DLYA CHAPTEROV
        //[HttpGet]
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        // //   Chapter chapter = CreativeService.GetChapter(id ?? 0);
        //    if (chapter == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(chapter);
        //}


        private ApplicationUser FindUser()
        {
            if (!System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                return null;
            return db.Users.Find(System.Web.HttpContext.Current.User.Identity.GetUserId());
        }



    }
}