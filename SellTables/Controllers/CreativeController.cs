using Microsoft.AspNet.Identity;
using SellTables.Models;
using SellTables.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SellTables.Controllers
{
    public class CreativeController : Controller
    {
       ApplicationDbContext db = new ApplicationDbContext();

        CreativeService CreativeService;
        public CreativeController() {
       CreativeService = new CreativeService();
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
        public ActionResult Create([Bind(Include = "Id,Name,Rating,CreationDate,UserId")] Creative creative)
        {
                if (ModelState.IsValid)
                {
                    creative.User = FindUser();
              CreativeService.AddCreative(creative, db);
                //    db.Creatives.Add(creative);
                //     db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(creative);
            
        }

     
        private ApplicationUser FindUser()
        {
         
                if (!System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                    return null;
                return db.Users.Find(System.Web.HttpContext.Current.User.Identity.GetUserId());
            }
            
        

    }
}