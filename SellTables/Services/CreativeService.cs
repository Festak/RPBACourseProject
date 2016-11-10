using SellTables.Interfaces;
using SellTables.Lucene;
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
        private ApplicationDbContext db;
        private IRepository<Creative> CreativeRepository;
        private IRepository<Chapter> ChapterRepository;
        private IRepository<Rating> RatingsRepository;
        private IRepository<Tag> TagsRepository;


        public CreativeService(ApplicationDbContext db)
        {
            this.db = db;
            CreativeRepository = new CreativesRepository(db);
            ChapterRepository = new ChaptersRepository(db);
            RatingsRepository = new RatingsRepository(db);
            TagsRepository = new TagsRepository(db);
        }

        internal List<CreativeViewModel> GetAllCreatives()
        {
            var listOfСreatives = InitCreatives(CreativeRepository.GetAll());
            return listOfСreatives.ToList();
        }

        internal List<Creative> GetAllCreativesForLucene()
        {
            var listOfСreatives = CreativeRepository.GetAll();
            return listOfСreatives.ToList();
        }


        internal void AddCreative(RegisterCreativeModel creativemodel)
        {
            Creative creative = creativemodel.Creative;
            Chapter chapter = creativemodel.Chapter;
            chapter.Creative = creative;
            chapter.Tags = GetTags(chapter.TagsString,chapter);
            creative.Chapters.Add(chapter);
         CreativeSearch.AddUpdateLuceneIndex(creative); //ADD LUCENE INDEX
            CreativeRepository.Add(creative);
            ChapterRepository.Add(chapter);
        }
 
        public ICollection<CreativeViewModel> GetCreativesBySearch(ICollection<CreativeViewModel> list) {
            List<Creative> creatives = new List<Creative>();
            foreach (var cr in list) {
                var creative = db.Creatives.FirstOrDefault(c => c.Name.Equals(cr.Name));
                creatives.Add(creative);
            }
            return InitCreativesBySearch(creatives);
          
        }

        private void AddTagsToDB(Chapter chapter) {
            foreach (var tag in chapter.Tags) {
                TagsRepository.Add(tag);
            }
          
        }

        private ICollection<Tag> GetTags(String tagList, Chapter chapter)
        {
            var stringList = tagList.Split(' ');
            var tags = new List<Tag>();
           
            if (stringList != null)
            {
                foreach (String text in stringList)
                {
                    Tag tag = new Tag();
                    tag.Chapters.Add(chapter);
                    tag.Description = text;
                    tags.Add(tag);
                }
            }
            return tags;
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


        private ICollection<CreativeViewModel> InitCreativesBySearch(ICollection<Creative> list)
        {
            return list.Select(creative => new CreativeViewModel
            {
                Id = creative.Id,
                Chapters = InitChaptersBySearch(creative.Chapters),
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
                Tags = c.Tags,
            }).ToList();
            return Chapters;
        }
        private ICollection<ChapterViewModel> InitChaptersBySearch(ICollection<Chapter> list)
        {
            var Chapters = list.Select(c => new ChapterViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Text = c.Text,
                Number = c.Number,
                Tags = GetTags(c.TagsString),
                TagString = c.TagsString
            }).ToList();
            return Chapters;
        }



        private static ICollection<Tag> GetTags(String tagList)
        {
            var stringList = tagList.Split(' ');
            var tags = new List<Tag>();
            if (stringList != null)
            {
                foreach (String text in stringList)
                    tags.Add(new Tag() { Description = text });
            }
            return tags;
        }

        private Creative InitCreative(CreativeViewModel creativemodel, ApplicationUser user) {
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


        internal List<CreativeViewModel> GetCreativesRange(int start, int count, int sortType)
        {
            var listOfUsers = InitCreatives(((CreativesRepository)CreativeRepository).GetRange(start, count, sortType, db));
            if (listOfUsers == null) {
                return null;
            }
            return listOfUsers.ToList();
        }

        internal void SetRatingToCreative(int rating, CreativeViewModel creativemodel, ApplicationUser user)
        {
            Rating ratingObj = new Rating();
            Creative creative = db.Creatives.Find(creativemodel.Id);
            ratingObj.Creative = creative;
            ratingObj.Value = rating;
            ratingObj.User = user;
            ratingObj.UserId = user.Id;
            CalculateRating(ratingObj, creative);
        }

        private void CalculateRating(Rating rating, Creative creative)
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
            
            CreativeRepository.Update(creative);
            RatingsRepository.Add(rating);
  
            
        }

        internal List<CreativeViewModel> GetPopularCreatives() {
            var listOfСreatives = InitCreatives(((CreativesRepository)CreativeRepository).GetPopular());
            return listOfСreatives.ToList();
        }


    }
}