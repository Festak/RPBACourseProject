﻿using SellTables.Interfaces;
using SellTables.Models;
using SellTables.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SellTables.Services
{
    public class ChapterService
    {
        private ApplicationDbContext db;
        private IRepository<Chapter> ChapterRepository ;
       
        public ChapterService(ApplicationDbContext db) {
            this.db = db;
            ChapterRepository = new ChaptersRepository(db);
        }

        internal Chapter GetChapter(int id)
        {
            return ChapterRepository.Get(id);
        }
    }
}