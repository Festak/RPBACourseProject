using MultilingualSite.Filters;
using SellTables.Models;
using SellTables.Services;
using System.Net;
using System.Web.Mvc;

namespace SellTables.Controllers
{
    [Culture]
    public class ChapterController : DefaultController
    {
        ChapterService ChapterService;
        ApplicationDbContext DataBaseConnection;

        public ChapterController(ApplicationDbContext DataBaseConnection) {
            this.DataBaseConnection = DataBaseConnection;
            ChapterService = new ChapterService(DataBaseConnection);
        }


        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chapter chapter = ChapterService.GetChapter(id ?? 0);
            if (chapter == null)
            {
                return HttpNotFound();
            }
            return View(chapter);
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}