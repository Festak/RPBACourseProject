using SellTables.Interfaces;
using SellTables.Models;
using SellTables.Repositories;
using SellTables.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SellTables.Services
{
    public class CreativeService
    {
        private IRepository<Creative> Repository;
        private IRepository<Chapter> ChapterRepository;


        public CreativeService()
        {
            Repository = new CreativesRepository();
            ChapterRepository = new ChaptersRepository();
        }

        internal List<CreativeViewModel> GetAllCreatives()
        {
            var listOfСreatives = InitCreatives(Repository.GetAll());
            return listOfСreatives.ToList();
        }


        internal void AddCreative(RegisterCreativeModel creativemodel, ApplicationDbContext db)
        {
            Creative creative = creativemodel.Creative;
            Chapter chapter = creativemodel.Chapter;
            chapter.Creative = creative;
            creative.Chapters.Add(chapter);
            Repository.Add(creative, db);
            ChapterRepository.Add(chapter, db);

        }


        private ICollection<CreativeViewModel> InitCreatives(ICollection<Creative> list)
        {
            return list.Select(creative => new CreativeViewModel
            {
                Id = creative.Id,
                Chapters = InitChapters(creative.Chapters),
                UserName = creative.User.UserName,
                Name = creative.Name,
                Rating = creative.Rating,
                CreationDate = creative.CreationDate.ToShortDateString() + " " + creative.CreationDate.ToShortTimeString()
            }).ToList();
        }

        private ICollection<ChapterViewModel> InitChapters(ICollection<Chapter> list)
        {
            var Chapters = list.Select(c => new ChapterViewModel
            {
                Name = c.Name,
                Text = c.Text,
                Number = c.Number,
                Tags = c.Tags
            }).ToList();
            return Chapters;
        }

        private CreativeViewModel InitCreative(Creative creative)
        {
            return null;
        }

        private ChapterViewModel InitChapter(Chapter chapter)
        {
            return null;
        }
        internal List<CreativeViewModel> GetCreativesRange(int start, int count, ApplicationDbContext db)
        {
            var listOfUsers = InitCreatives(((CreativesRepository)Repository).GetRange(start, count, db));
            if (listOfUsers == null)
            {
                return null;
            }
            return listOfUsers.ToList();
        }

    }
}