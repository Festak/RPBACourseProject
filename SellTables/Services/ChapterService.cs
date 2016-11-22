using SellTables.Interfaces;
using SellTables.Models;
using SellTables.Repositories;

using System.Collections.Generic;


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

       public Chapter GetChapter(int id)
        {
            return ChapterRepository.Get(id);
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





    }
}