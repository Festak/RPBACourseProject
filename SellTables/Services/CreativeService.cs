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
        private static IRepository<Creative> Repository;

        public CreativeService()
        {
            Repository = new CreativesRepository();
        }

        internal static List<CreativeViewModel> GetAllCreatives()
        {
            var listOfСreatives = InitCreatives(Repository.GetAll());
         //   var listOfСreatives = (Repository.GetAll());
            return listOfСreatives.ToList();
        }


        private static ICollection<CreativeViewModel> InitCreatives(ICollection<Creative> list) {
            return list.Select(creative => new CreativeViewModel
            {
               Id = creative.Id,
                Chapters = creative.Chapters,
                UserName = creative.User.UserName,
                Name = creative.Name,
                Rating = creative.Rating,
                CreationDate = creative.CreationDate.ToShortDateString() + " " + creative.CreationDate.ToShortTimeString()
            }).ToList();
        }

        private ICollection<ChapterViewModel> InitChapters(ICollection<Chapter> list)
        {

            return null;
        }

        private CreativeViewModel InitCreative(Creative creative) {
            return null;
        }

        private ChapterViewModel InitChapter(Chapter chapter) {
            return null;
        }
    }
}