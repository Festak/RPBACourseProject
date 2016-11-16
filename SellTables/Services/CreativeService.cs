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
using System.Data.Entity;

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

        public List<CreativeViewModel> GetAllCreatives()
        {
            var listOfСreatives = InitCreatives(GetAllCreativesFromDataBase());
            return listOfСreatives.ToList();
        }

        public List<Creative> GetAllCreativesForLucene()
        {
            return GetAllCreativesFromDataBase();
        }

        public void AddCreative(RegisterCreativeModel creativemodel)
        {
            Creative creative = creativemodel.Creative;
            AddCreativeToCounter(creative.User.Id);
            Chapter chapter = creativemodel.Chapter;
            chapter.Creative = creative;
            if (chapter.TagsString != null)
                chapter.Tags = GetTags(chapter.TagsString, chapter);
            creative.Chapters.Add(chapter);
            CreativeSearch.AddUpdateLuceneIndex(creative); //ADD LUCENE INDEX
            CreativeRepository.Add(creative);
            ChapterRepository.Add(chapter);
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

        public List<CreativeViewModel> GetCreativesRange(int start, int count, int sortType)
        {
            var listOfUsers = InitCreatives(((CreativesRepository)CreativeRepository).GetRange(start, count, sortType));
            if (listOfUsers == null)
            {
                return null;
            }
            return listOfUsers.ToList();
        }

        public Creative GetCreative(int id)
        {
            return CreativeRepository.Get(id);
        }

        public List<Creative> GetAllCreativesModels()
        {
            return GetAllCreativesFromDataBase();
        }

        public List<CreativeViewModel> GetPopularCreatives()
        {
            var listOfСreatives = InitCreatives(((CreativesRepository)CreativeRepository).GetPopular());
            return listOfСreatives.ToList();
        }

        public List<CreativeViewModel> GetCreativesByUser(string userName)
        {
            var user = UsersRepository.FindUser(userName);
            var listOfCreatives = InitCreatives(CreativeRepository.GetAll().Where(u => u.User == user).ToList());
            return listOfCreatives.ToList();
        }

        public void DeleteCreativeById(int id, string userName)
        {
            var creative = dataBaseContext.Creatives.Include(c => c.Chapters).FirstOrDefault(i => i.Id == id);
            ApplicationUser user = UsersRepository.FindUser(userName);
            user.Creatives.Remove(creative);
            DeleteAllCreativesRating(creative);
            DeleteAllChaptersFromCreative(creative);
            if (creative != null)
            {
                dataBaseContext.Creatives.Remove(creative);
                CreativeSearch.ClearLuceneIndexRecord(creative.Id);
            }
            dataBaseContext.SaveChanges();
        }

        public void SetRatingToCreative(int rating, CreativeViewModel creativemodel, ApplicationUser user)
        {
            Creative creative = dataBaseContext.Creatives.Find(creativemodel.Id);
            Rating ratingObj = InitRating(creative, rating, user);
            CalculateRating(ratingObj, creative);
        }

        public ICollection<Rating> GetAllCreativeRatings(Creative creative)
        {
            if (creative == null)
                return new List<Rating>();
            return dataBaseContext.Rating
                        .Where(r => r.CreativeId == creative.Id).ToList();
        }

        private List<Creative> GetAllCreativesFromDataBase()
        {
            var listOfCreatives = CreativeRepository.GetAll();
            return listOfCreatives.ToList();
        }

        private void AddCreativeToUser(Creative creative)
        {
            ApplicationUser user = UsersRepository.FindUserById(creative.User.Id);
            user.Creatives.Add(creative);
            UsersRepository.UpdateUser(user);
        }

        private void AddCreativeToCounter(string userId)
        {
            ApplicationUser user = UsersRepository.FindUserById(userId);
            user.ChaptersCreateCounter += 1;
            if (user.ChaptersCreateCounter == 1) // TODO: make verification for medal exist
            {
                Medal medal = dataBaseContext.Medals.FirstOrDefault(m => m.Id == 3);
                if (!user.Medals.Contains(medal))
                    user.Medals.Add(medal);

            }
            if (user.ChaptersCreateCounter == 5)
            {
                Medal medal = dataBaseContext.Medals.FirstOrDefault(m => m.Id == 4);
                if (!user.Medals.Contains(medal))
                    user.Medals.Add(medal);
            }
            UsersRepository.UpdateUser(user);
            dataBaseContext.SaveChanges();
        }


        private void AddTagsTodataBaseContext(Chapter chapter)
        {
            foreach (var tag in chapter.Tags)
            {
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
                    if (dataBaseContext.Tags.Where(t => t.Description == text).ToList().Count == 0)
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
                Medals = InitMedals(creative.User.Medals),
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
                Medals = InitMedals(creative.User.Medals),
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


        private ICollection<MedalViewModel> InitMedals(ICollection<Medal> list)
        {
            var medals = list.Select(c => new MedalViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                ImageUri = c.ImageUri
            }).ToList();
            return medals;
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

        private Rating InitRating(Creative creative, int rating, ApplicationUser user)
        {
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

        private void DeleteAllChaptersFromCreative(Creative creative)
        {
            foreach (var chapter in creative.Chapters.ToList())
            {
                DeleteTagFromChapter(chapter);
                var c = dataBaseContext.Chapters.Find(chapter.Id);
                if (c != null)
                {
                    dataBaseContext.Chapters.Remove(c);

                }
            }
            dataBaseContext.SaveChanges();
        }

        private void DeleteTagFromChapter(Chapter chapter)
        {
            foreach (var tag in chapter.Tags.ToList())
            {
                var t = dataBaseContext.Tags.Find(tag.Id);
                if (t != null)
                    dataBaseContext.Tags.Remove(t);

            }
            dataBaseContext.SaveChanges();
        }


        private void DeleteAllCreativesRating(Creative creative)
        {
            var ratings = GetAllCreativeRatings(creative);
            if (ratings != null)
            {
                dataBaseContext.Rating.RemoveRange(ratings);
                dataBaseContext.SaveChanges();
            }
        }



    }
}