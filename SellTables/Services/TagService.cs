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
    public class TagService
    {
        private IRepository<Tag> Repository;
        private ApplicationDbContext db;

        public TagService(ApplicationDbContext db)
        {
            this.db = db;
            Repository = new TagsRepository(db);
        }

        internal List<Tag> GetAllTags()
        {
            var listOfTags = (Repository.GetAll());
            return listOfTags.ToList();
        }


        internal ICollection<TagViewModel> GetMostPopularTags(int number = 10)
        {
            var mostPopular = new int[number];
            var result = new String[number];
            string pTag = "";
            if (GetAllTags() == null)
                return null;
            foreach (Tag tag in GetAllTags())
                for (int i = 0; i < number; i++)
                {
                    var newTagRepeats = GetTagRepeatNumber(tag);
                    if (result.Contains(null))
                    {
                        var index = Array.IndexOf(result, null);
                        result[index] = tag.Description;
                        pTag += result[index] + " ";
                        mostPopular[index] = newTagRepeats;
                        break;
                    }
                    if (newTagRepeats > mostPopular[i])
                    {
                        mostPopular[i] = newTagRepeats;
                        result[i] = tag.Description;
                        pTag += result[i] + " ";
                        break;
                    }
                }
            return InitTagModel(GetTags(pTag));
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

        private int GetTagRepeatNumber(Tag tag)
        {
            if (tag == null || FindTag(tag.Description) == null)
                return 0;
            var findTag = FindTag(tag.Description);
            return findTag.Chapters.Count;
        }

        private Tag FindTag(String name)
        {
            return db.Tags
                    .Where(t => t.Description.Equals(name))
                    .FirstOrDefault();
        }


        internal List<TagViewModel> GetAllModelTags()
        {
            var listOfTags = InitTagModel(Repository.GetAll());
            return listOfTags.ToList();
        }

        private ICollection<TagViewModel> InitTagModel(ICollection<Tag> list)
        {
            return list.Select(tag => new TagViewModel
            {
                Id = tag.Id,
                Description = tag.Description
            }).ToList();
        }

    }
}