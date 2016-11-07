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

namespace SellTables.Controllers
{
    public class RatingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Ratings
        public async Task<ActionResult> Index()
        {
            var rating = db.Rating.Include(r => r.Chapter);
            return View(await rating.ToListAsync());
        }

        // GET: Ratings/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = await db.Rating.FindAsync(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            return View(rating);
        }

        // GET: Ratings/Create
        public ActionResult Create()
        {
            ViewBag.ChapterId = new SelectList(db.Chapters, "Id", "Name");
            return View();
        }

        // POST: Ratings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Value,ChapterId")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                db.Rating.Add(rating);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ChapterId = new SelectList(db.Chapters, "Id", "Name", rating.ChapterId);
            return View(rating);
        }

        // GET: Ratings/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = await db.Rating.FindAsync(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            ViewBag.ChapterId = new SelectList(db.Chapters, "Id", "Name", rating.ChapterId);
            return View(rating);
        }

        // POST: Ratings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Value,ChapterId")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rating).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ChapterId = new SelectList(db.Chapters, "Id", "Name", rating.ChapterId);
            return View(rating);
        }

        // GET: Ratings/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = await db.Rating.FindAsync(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            return View(rating);
        }

        // POST: Ratings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Rating rating = await db.Rating.FindAsync(id);
            db.Rating.Remove(rating);
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
