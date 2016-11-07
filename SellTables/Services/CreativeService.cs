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

        public CreativeService()
        {
            Repository = new CreativesRepository();
        }

        internal List<CreativeViewModel> GetAllCreatives()
        {
            var listOfСreatives = InitCreatives(Repository.GetAll());
            return listOfСreatives.ToList();
        }


        internal void AddCreative(Creative creative, ApplicationDbContext db) {
            Repository.Add(creative, db);
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
        internal static List<Creative> GetCreativesRange(int start, int count)
        {
            var listOfUsers = ((CreativesRepository)Repository).GetRange(start, count);
            if (listOfUsers == null) {
                return null;
            }
            return listOfUsers.ToList();
        }

    }
}