using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SellTables.Models;
using Microsoft.AspNet.Identity;

namespace SellTables.Controllers
{
    public class CreativesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Creatives
        public async Task<ActionResult> Index()
        {
            var creatives = db.Creatives.Include(c => c.User);
            return View(await creatives.ToListAsync());
        }

        // GET: Creatives/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Creative creative = await db.Creatives.FindAsync(id);
            if (creative == null)
            {
                return HttpNotFound();
            }
            return View(creative);
        }

        // GET: Creatives/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Rating,CreationDate,UserId")] Creative creative)
        {
            if (ModelState.IsValid)
            {
                creative.User = getCurrentUser();
              db.Creatives.Add(creative);
              //  await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "UserName", creative.UserId);
            return View(creative);
        }

        public ApplicationUser getCurrentUser()
        {
            if (!System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                return null;
            return db.Users.Find(System.Web.HttpContext.Current.User.Identity.GetUserId());
        }

        // GET: Creatives/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Creative creative = await db.Creatives.FindAsync(id);
            if (creative == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserName", creative.UserId);
            return View(creative);
        }

        // POST: Creatives/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Rating,CreationDate,UserId")] Creative creative)
        {
            if (ModelState.IsValid)
            {
                db.Entry(creative).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserName", creative.UserId);
            return View(creative);
        }

        // GET: Creatives/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Creative creative = await db.Creatives.FindAsync(id);
            if (creative == null)
            {
                return HttpNotFound();
            }
            return View(creative);
        }

        // POST: Creatives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Creative creative = await db.Creatives.FindAsync(id);
            db.Creatives.Remove(creative);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
