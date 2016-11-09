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
        private IRepository<Creative> CreativeRepository;
        private IRepository<Chapter> ChapterRepository;
        private IRepository<Rating> RatingsRepository;


        public CreativeService()
        {
            CreativeRepository = new CreativesRepository();
            ChapterRepository = new ChaptersRepository();
            RatingsRepository = new RatingsRepository();
        }

        internal List<CreativeViewModel> GetAllCreatives()
        {
            var listOfСreatives = InitCreatives(CreativeRepository.GetAll());
            return listOfСreatives.ToList();
        }


        internal void AddCreative(RegisterCreativeModel creativemodel, ApplicationDbContext db)
        {
            Creative creative = creativemodel.Creative;
            Chapter chapter = creativemodel.Chapter;
            chapter.Creative = creative;
            creative.Chapters.Add(chapter);
            CreativeRepository.Add(creative, db);
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
                Id = c.Id,
                Name = c.Name,
                Text = c.Text,
                Number = c.Number,
                Tags = c.Tags
            }).ToList();
            return Chapters;
        }

        private Creative InitCreative(CreativeViewModel creativemodel, ApplicationUser user, ApplicationDbContext db) {
            Creative creative = db.Creatives.Find(creativemodel.Id);
           return creative;
        }

        private ICollection<Chapter> InitChapters(ICollection<ChapterViewModel> list)
        {
            var Chapters = list.Select(c => new Chapter
            {
                Id = c.Id,
                CreativeId = c.CreativeId,
                Name = c.Name,
                Text = c.Text,
                Number = c.Number,
                Tags = c.Tags
            }).ToList();
            return Chapters;
        }


        internal List<CreativeViewModel> GetCreativesRange(int start, int count, int sortType, ApplicationDbContext db)
        {
            var listOfUsers = InitCreatives(((CreativesRepository)CreativeRepository).GetRange(start, count, sortType, db));
            if (listOfUsers == null) {
                return null;
            }
            return listOfUsers.ToList();
        }

        internal void SetRatingToCreative(int rating, CreativeViewModel creativemodel, ApplicationDbContext db, ApplicationUser user)
        {
            Rating ratingObj = new Rating();
            Creative creative = db.Creatives.Find(creativemodel.Id);
            ratingObj.Creative = creative;
            ratingObj.Value = rating;
            ratingObj.User = user;
            ratingObj.UserId = user.Id;
            CalculateRating(ratingObj, creative, db);
        }

        private void CalculateRating(Rating rating, Creative creative, ApplicationDbContext db)
        {
            double a = 0;
            creative.Ratings.Add(rating);
            foreach (var r in creative.Ratings)
            {
                a += r.Value;
            }
            //if (creative.Ratings.Count != 0)
            //{
                a /= creative.Ratings.Count;
            //}
            //else {
            //    a = ra / 1;
            //}
            creative.Rating = Math.Round(a,2);
            
            CreativeRepository.Update(creative, db);
            RatingsRepository.Add(rating, db);
  
            
        }

        internal List<CreativeViewModel> GetPopularCreatives() {
            var listOfСreatives = InitCreatives(((CreativesRepository)CreativeRepository).GetPopular());
            return listOfСreatives.ToList();
        }


    }
}