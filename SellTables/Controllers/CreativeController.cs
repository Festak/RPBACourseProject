using Microsoft.AspNet.Identity;
using MultilingualSite.Filters;
using SellTables.Models;
using SellTables.Services;
using SellTables.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SellTables.Controllers
{
    [Culture]
    [Authorize]
    public class CreativeController : DefaultController
    {
        ApplicationDbContext dataBaseConnection = new ApplicationDbContext();

        private CreativeService CreativeService;
        private ChapterService ChapterService;
        private CloudinaryService CloudinaryService;

        public CreativeController()
        {
            CreativeService = new CreativeService(dataBaseConnection);
            ChapterService = new ChapterService(dataBaseConnection);
            CloudinaryService = new CloudinaryService(dataBaseConnection);

        }

        [AllowAnonymous]
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
                string path = CloudinaryService.UploadCreativeImage(creativemodel.Image);
                creativemodel.Creative.CreativeUri = path;
                CreativeService.AddCreative(creativemodel);
                return RedirectToAction("Index");
            }

            return View(creativemodel);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Creative creative = CreativeService.GetCreative(id ?? 0);
            if (creative == null)
            {
                return HttpNotFound();
            }
            return View(creative);
        }

        [HttpPost]
        public void UpdateCreativeName(int id, string newName) {
            CreativeService.UpdateCreativeName(id, newName);
        }

        [HttpPost]
        public void UpdateCreativeImage(int id, byte[] image) {
            string path = CloudinaryService.UploadImage(image);
            CreativeService.UpdateCreativeImage(id, path);
        }

        [HttpGet]
        public ActionResult EditChapter(int creativeId, int chapterId)
        {
            return View(new RegisterCreativeModel() { creativeId = creativeId, chapterId = chapterId, Chapter = ChapterService.GetChapter(chapterId) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditChapter(RegisterCreativeModel model)
        {
            if (ModelState.IsValid)
            {
                CreativeService.EditCreativeChapter(model);
                return RedirectToAction("UserPage", "User", new { area = "" });
            }
            return View(model);
        }

        public void GetRatingFromView(int rating, CreativeViewModel creative)
        {
            CreativeService.SetRatingToCreative(rating, creative, FindUser());
        }

        private ApplicationUser FindUser()
        {
            if (!System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                return null;
            return dataBaseConnection.Users.Find(System.Web.HttpContext.Current.User.Identity.GetUserId());
        }

        public void DeleteChapterById(int id, string userName) {
            CreativeService.DeleteChapterById(id, userName);
        }

        [AllowAnonymous]
        public ActionResult Search(string query)
        {
            if (query == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var listOfLuceneObjectsByTags = Lucene.CreativeSearch.Search(query);  
            return View(listOfLuceneObjectsByTags.ToList());
        }

        [AllowAnonymous]
        public JsonResult GetCreativesByUser(string userName)
        {
            var creatives = CreativeService.GetCreativesByUser(userName);
            return Json(creatives, JsonRequestBehavior.AllowGet);
        }

        public void DeleteCreativeById(int id, string userName)
        {
            CreativeService.DeleteCreativeById(id, userName);
        }

        [HttpGet]
        public ActionResult CreateChapter(int? creativeId)
        {
            RegisterCreativeModel model = new RegisterCreativeModel()
            {
                creativeId = creativeId ?? 0
            };
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateChapter(RegisterCreativeModel model)
        {
            if (ModelState.IsValid)
            {
                model.Creative = CreativeService.GetCreativeById(model.creativeId);
                CreativeService.AddChapterToCreative(model);
                return RedirectToAction("UserPage", "User");
            }
            return View(model);
        }

        public void UpdateChapterPos(int oldPosition,int newPosition, int fromChapterId, int toChapterId) {
           ChapterService.UpdateChapterPos(oldPosition, newPosition, fromChapterId, toChapterId);
        }

    }
}