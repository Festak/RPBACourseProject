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


        internal List<CreativeViewModel> GetCreativesRange(int start, int count, ApplicationDbContext db)
        {
            var listOfUsers = InitCreatives(((CreativesRepository)CreativeRepository).GetRange(start, count, db));
            if (listOfUsers == null)
            {
                return null;
            }
            return listOfUsers.ToList();
        }

        internal void SetRatingToCreative(int rating, Creative creative, ApplicationDbContext db, ApplicationUser user)
        {
            Rating ratingObj = new Rating();
            int count = db.Rating.Where(u => u.User == user).Where(c => c.Creative == creative).Count();
            if (count == 0)
            {
                ratingObj.User = user;
                ratingObj.Creative = creative;
                ratingObj.CreativeId = creative.Id;
                ratingObj.Value = rating;
                creative.Ratings.Add(ratingObj);
                CalculateRating(ratingObj, creative, db);
            }
        }

        private void CalculateRating(Rating rating, Creative creative, ApplicationDbContext db)
        {
            double a = 0;
            foreach (var r in creative.Ratings)
            {
                a += r.Value;
            }
            a /= creative.Ratings.Count;
            creative.Rating = a;
            RatingsRepository.Add(rating, db);
            CreativeRepository.Update(creative, db);
        }

        internal List<CreativeViewModel> GetPopularCreatives() {
            var listOfСreatives = InitCreatives(((CreativesRepository)CreativeRepository).GetPopular());
            return listOfСreatives.ToList();
        }


    }
}