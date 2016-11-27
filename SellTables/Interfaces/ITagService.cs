using SellTables.Models;
using SellTables.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SellTables.Interfaces
{
    public interface ITagService
    {
        List<Tag> GetAllTags();
        List<TagViewModel> GetAllModelTags();
        ICollection<string> GetMostPopularTags(int number = 20);
    }
}