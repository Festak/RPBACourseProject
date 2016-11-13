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
        private ApplicationDbContext dataBaseContext;
        private IRepository<Creative> CreativeRepository;
        private IRepository<Chapter> ChapterRepository;
        private IRepository<Rating> RatingsRepository;
        private IRepository<Tag> TagsRepository;
        private IUserRepository UsersRepository;


        public CreativeService(ApplicationDbContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
            CreativeRepository = new CreativesRepository(dataBaseContext);
            ChapterRepository = new ChaptersRepository(dataBaseContext);
            RatingsRepository = new RatingsRepository(dataBaseContext);
            TagsRepository = new TagsRepository(dataBaseContext);
            UsersRepository = new UsersRepository(dataBaseContext);
        }

        internal List<CreativeViewModel> GetAllCreatives()
        {
            var listOfСreatives = InitCreatives(GetAllCreativesFromDataBase());
            return listOfСreatives.ToList();
        }

        internal List<Creative> GetAllCreativesForLucene()
        {
            return GetAllCreativesFromDataBase();
        }

        public List<Creative> GetAllCreativesModels() {
          return  GetAllCreativesFromDataBase();
        }

        private List<Creative> GetAllCreativesFromDataBase() {
            var listOfCreatives = CreativeRepository.GetAll();
            return listOfCreatives.ToList();
        }


        internal void AddCreative(RegisterCreativeModel creativemodel)
        {
            Creative creative = creativemodel.Creative;
            AddCreativeToCounter(creative.User.Id);
            Chapter chapter = creativemodel.Chapter;
            chapter.Creative = creative;
            if(chapter.TagsString!=null)
            chapter.Tags = GetTags(chapter.TagsString, chapter);
            creative.Chapters.Add(chapter);
          //  AddCreativeToUser(creative);
           
            CreativeSearch.AddUpdateLuceneIndex(creative); //ADD LUCENE INDEX
            CreativeRepository.Add(creative);
            ChapterRepository.Add(chapter);
        }


        private void AddCreativeToUser(Creative creative) {
            ApplicationUser user = UsersRepository.FindUserById(creative.User.Id);
            user.Creatives.Add(creative);
            UsersRepository.UpdateUser(user);
        }

        public ICollection<CreativeViewModel> GetCreativesBySearch(ICollection<CreativeViewModel> list)
        {
            List<Creative> creatives = new List<Creative>();
            foreach (var cr in list)
            {
                var creative = dataBaseContext.Creatives.FirstOrDefault(c => c.Name.Equals(cr.Name));
                creatives.Add(creative);
            }
            return InitCreativesBySearch(creatives);
        }

        private void AddCreativeToCounter(string userId)
        {
            ApplicationUser user = UsersRepository.FindUserById(userId);
            user.ChaptersCreateCounter += 1;
            if (user.ChaptersCreateCounter == 5) // TODO: make verification for medal exist
            {
                user.Medals.Add(dataBaseContext.Medals.FirstOrDefault(m => m.Id == 2));
            }
            UsersRepository.UpdateUser(user);
            dataBaseContext.SaveChanges();
        }

        internal Creative GetCreative(int id)
        {
            return CreativeRepository.Get(id);
        }

        private void AddTagsTodataBaseContext(Chapter chapter) {
            foreach (var tag in chapter.Tags) {
                TagsRepository.Add(tag);
            }

        }

        private ICollection<Tag> GetTags(string tagList, Chapter chapter)
        {
            var stringList = tagList.Split(' ');
            var tags = new List<Tag>();

            if (stringList != null)
            {
                foreach (string text in stringList)
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
                Medals = creative.User.Medals,
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

        private static ICollection<Tag> GetTags(string tagList)
        {
            var stringList = tagList.Split(' ');
            var tags = new List<Tag>();
            if (stringList != null)
            {
                foreach (string text in stringList)
                    tags.Add(new Tag() { Description = text });
            }
            return tags;
        }

        private Creative InitCreative(CreativeViewModel creativemodel, ApplicationUser user)
        {
            Creative creative = dataBaseContext.Creatives.Find(creativemodel.Id);
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
            var listOfUsers = InitCreatives(((CreativesRepository)CreativeRepository).GetRange(start, count, sortType));
            if (listOfUsers == null)
            {
                return null;
            }
            return listOfUsers.ToList();
        }

        internal void SetRatingToCreative(int rating, CreativeViewModel creativemodel, ApplicationUser user)
        {
            Creative creative = dataBaseContext.Creatives.Find(creativemodel.Id);
            Rating ratingObj = InitRating(creative, rating, user);
            CalculateRating(ratingObj, creative);
        }

        private Rating InitRating(Creative creative, int rating, ApplicationUser user) {
            Rating ratingObj = new Rating();
            ratingObj.Creative = creative;
            ratingObj.Value = rating;
            ratingObj.User = user;
            ratingObj.UserId = user.Id;
            return ratingObj;
        }

        private void CalculateRating(Rating rating, Creative creative)
        {
            double a = 0;
            creative.Ratings.Add(rating);
            foreach (var r in creative.Ratings)
            {
                a += r.Value;
            }
            a /= creative.Ratings.Count;
            creative.Rating = Math.Round(a, 2);
            CreativeRepository.Update(creative);
            RatingsRepository.Add(rating);
        }

        internal List<CreativeViewModel> GetPopularCreatives()
        {
            var listOfСreatives = InitCreatives(((CreativesRepository)CreativeRepository).GetPopular());
            return listOfСreatives.ToList();
        }

        internal List<CreativeViewModel> GetCreativesByUser(string userName)
        {
            var user = UsersRepository.FindUser(userName);
            var listOfCreatives = InitCreatives(CreativeRepository.GetAll().Where(u => u.User == user).ToList());
            return listOfCreatives.ToList();
        }




    }
}