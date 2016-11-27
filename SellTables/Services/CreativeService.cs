using SellTables.Interfaces;
using SellTables.Lucene;
using SellTables.Models;
using SellTables.Repositories;
using SellTables.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Data.Entity;

namespace SellTables.Services
{
    public class CreativeService : ICreativeService
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
            if (chapter.TagsString != null)
                chapter.Tags = GetTags(chapter.TagsString);
            AddCreativeAndChapterToDatabase(creative, chapter);
        }

        public ICollection<CreativeViewModel> GetCreativesBySearch(ICollection<CreativeViewModel> list)
        {
            List<Creative> creatives = new List<Creative>();
            foreach (var cr in list)
            {
                var creative = dataBaseContext.Creatives.FirstOrDefault(c => c.Name == cr.Name);
                creatives.Add(creative);
            }
            if (creatives != null)
                return InitCreativesBySearch(creatives);
            else return new List<CreativeViewModel>();
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

        public List<CreativeViewModel> GetLastEditedCreatives()
        {
            var listOfCreatives = InitCreatives(((CreativesRepository)CreativeRepository).GetLastEdited());
            return listOfCreatives.ToList();
        }

        public void DeleteChapterById(int id, string userName)
        {
            var chapter = dataBaseContext.Chapters.Include(t => t.Tags).FirstOrDefault(i => i.Id == id);
            DeleteTagFromChapter(chapter);
            dataBaseContext.Chapters.Remove(chapter);
            dataBaseContext.SaveChanges();
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

            bool isUserNotVoted = IsUserNotVoted(user, creativemodel.Id);
            if (isUserNotVoted)
            {
                Creative creative = dataBaseContext.Creatives.Find(creativemodel.Id);
                Rating ratingObj = InitRating(creative, rating, user);
                CalculateRating(ratingObj, creative);
            }
        }



        public void AddChapterToCreative(RegisterCreativeModel model)
        {
            Creative creative = model.Creative;
            Chapter chapter = model.Chapter;
            int lastIndex = GetLastIndexOfChapter(creative);
            chapter.Creative = creative;
            chapter.Number = lastIndex + 1;
            if (chapter.TagsString != null)
                chapter.Tags = GetTags(chapter.TagsString);
            UpdateDBAfterAddChapterToCreative(creative,chapter);
        }

        public void EditCreativeChapter(RegisterCreativeModel model)
        {
            Chapter chapter = BuildChapterByRegisterModel(model);
            Creative creative = CreativeRepository.Get(model.creativeId);
            creative.EditDate = DateTime.Now;
            CreativeRepository.Update(creative);
            ChapterRepository.Update(chapter);
        }

        public void UpdateCreativeName(int id, string name)
        {
            Creative creative = CreativeRepository.Get(id);
            creative.Name = name;
            creative.EditDate = DateTime.Now;
            CreativeRepository.Update(creative);
        }

        public void UpdateCreativeImage(int id, string path) {
            Creative creative = CreativeRepository.Get(id);
            creative.CreativeUri = path;
            CreativeRepository.Update(creative);
        }

        public ICollection<Rating> GetAllCreativeRatings(Creative creative)
        {
            if (creative == null)
                return new List<Rating>();
            return dataBaseContext.Rating
                        .Where(r => r.CreativeId == creative.Id).ToList();
        }

        public Creative GetCreativeById(int creativeId)
        {
            return CreativeRepository.Get(creativeId);
        }

        private List<Creative> GetAllCreativesFromDataBase()
        {
            var listOfCreatives = CreativeRepository.GetAll();
            return listOfCreatives.ToList();
        }

        private bool IsUserNotVoted(ApplicationUser user, int creativeId)
        {
            int count = dataBaseContext.Rating
                .Where(r => r.CreativeId == creativeId && r.UserId == user.Id)
                .Count();
            if (count == 0) return true;
            else return false;
        }

        private void AddCreativeAndChapterToDatabase(Creative creative, Chapter chapter)
        {
            chapter.Number = 0;
            creative.Chapters.Add(chapter);
            ChapterRepository.Add(chapter);
            CreativeRepository.Add(creative);
        }

        private void AddCreativeToUser(Creative creative)
        {
            ApplicationUser user = UsersRepository.FindUserById(creative.User.Id);
            user.Creatives.Add(creative);
            UsersRepository.UpdateUser(user);
        }

        private Chapter BuildChapterByRegisterModel(RegisterCreativeModel model)
        {
            Chapter chapter = ChapterRepository.Get(model.chapterId);
            chapter.Text = model.Chapter.Text;
            chapter.Name = model.Chapter.Name;
            if (model.Chapter.TagsString != null)
            {
                chapter.TagsString = model.Chapter.TagsString;
                chapter.Tags = GetTags(model.Chapter.TagsString);
            }
            return chapter;
        }

        private int GetLastIndexOfChapter(Creative creative)
        {
            Creative currentCreative = dataBaseContext.Creatives
                .Include(c => c.Chapters)
                .FirstOrDefault(i => i.Id == creative.Id);
            int lastIndexOfChapter = currentCreative.Chapters.Count - 1; // start from 0 at ng-sortable lib
            return lastIndexOfChapter;
        }

        private void AddCreativeToCounter(string userId)
        {
            ApplicationUser user = UsersRepository.FindUserById(userId);
            user.ChaptersCreateCounter += 1;
            user = CheckUserChaptersCount(user);
            UsersRepository.UpdateUser(user);
            dataBaseContext.SaveChanges();
        }

        private ApplicationUser CheckUserChaptersCount(ApplicationUser user)
        {
            if (user.ChaptersCreateCounter == 1)
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
            return user;
        }

        private void AddTagsTodataBaseContext(Chapter chapter)
        {
            foreach (var tag in chapter.Tags)
            {
                TagsRepository.Add(tag);
            }
        }

        private ICollection<Tag> GetTags(string tagList)
        {
            var stringList = tagList.Split('/');
            var tags = new List<Tag>();

            if (stringList != null)
            {
                tags = GetListOfTagsBySplit(stringList);
            }
            return tags;
        }

        private List<Tag> GetListOfTagsBySplit(string[] stringList)
        {
            var tags = new List<Tag>();
            foreach (string text in stringList)
            {
                if (text == "") continue;
                Tag tag = new Tag();
                tag.Description = text;
                AddTagToDataBaseByCondition(tag, text);
                tags.Add(tag);
            }
            return tags;
        }

        private void AddTagToDataBaseByCondition(Tag tag, string text) {
            if (dataBaseContext.Tags
                .Where(t => t.Description == text).ToList().Count == 0)
            {
                dataBaseContext.Tags.Add(tag);
                dataBaseContext.SaveChanges();
            }
        }

        private void UpdateDBAfterAddChapterToCreative(Creative creative, Chapter chapter)
        {
            creative.Chapters.Add(chapter);
            ChapterRepository.Add(chapter);
            CreativeRepository.Update(creative);
        }

        private ICollection<CreativeViewModel> InitCreatives(ICollection<Creative> list)
        {
            if (list != null)
            {
                return list.Select(creative => new CreativeViewModel
                {
                    Id = creative.Id,
                    Chapters = InitChapters(creative.Chapters),
                    UserName = creative.User.UserName,
                    UserUri = creative.User.AvatarUri,
                    Name = creative.Name,
                    EditDate = creative.EditDate.ToShortDateString() + " " + creative.EditDate.ToShortTimeString(),
                    Rating = creative.Rating,
                    Medals = InitMedals(creative.User.Medals),
                    CreativeUri = creative.CreativeUri,
                    CreationDate = creative.CreationDate.ToShortDateString() + " " + creative.CreationDate.ToShortTimeString()
                }).ToList();
            }
            else
            {
                return new List<CreativeViewModel>();
            }
        }

        private ICollection<CreativeViewModel> InitCreativesBySearch(ICollection<Creative> list)
        {
            if (list != null)
            {
                return list.Select(creative => new CreativeViewModel
                {
                    Id = creative.Id,
                    Chapters = InitChaptersBySearch(creative.Chapters),
                    UserName = creative.User.UserName,
                    Name = creative.Name,
                    EditDate = (creative.EditDate.ToString()),
                    Rating = creative.Rating,
                    Medals = InitMedals(creative.User.Medals),
                    CreativeUri = creative.CreativeUri,
                    CreationDate = creative.CreationDate.ToShortDateString() + " " + creative.CreationDate.ToShortTimeString()
                }).ToList();
            }
            else
            {
                return new List<CreativeViewModel>();
            }
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
            UpdateDataBaseAfterCalculateRating(creative, rating);
        }

        private void UpdateDataBaseAfterCalculateRating(Creative creative, Rating rating) {
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
                {
                    dataBaseContext.Tags.Remove(t);
                }
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