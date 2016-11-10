﻿using SellTables.Models;
using SellTables.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SellTables.Controllers
{
    public class ChapterController : Controller
    {
        ChapterService ChapterService;
        ApplicationDbContext db = new ApplicationDbContext();
        public ChapterController() {
            ChapterService = new ChapterService(db);
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