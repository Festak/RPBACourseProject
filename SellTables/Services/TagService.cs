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

        public TagService()
        {
            Repository = new TagsRepository();
        }

        internal List<Tag> GetAllTags()
        {
            var listOfTags = (Repository.GetAll());
            return listOfTags.ToList();
        }

        internal List<TagViewModel> GetAllModelTags()
        {
            var listOfTags = InitCreatives(Repository.GetAll());
            return listOfTags.ToList();
        }

        private ICollection<TagViewModel> InitCreatives(ICollection<Tag> list)
        {
            return list.Select(tag => new TagViewModel
            {
                Id = tag.Id,
                Description = tag.Description
              
            }).ToList();
        }

    }
}