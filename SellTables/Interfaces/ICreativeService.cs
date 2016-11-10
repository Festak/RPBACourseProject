using SellTables.Models;
using SellTables.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SellTables.Interfaces
{
    public interface ICreativeService
    {
        List<CreativeViewModel> GetAllCreatives();
        List<Creative> GetAllCreativesForLucene();
        void AddCreative(RegisterCreativeModel creativemodel);
        ICollection<CreativeViewModel> GetCreativesBySearch(ICollection<CreativeViewModel> list);
        ICollection<CreativeViewModel> InitCreatives(ICollection<Creative> list);
        ICollection<CreativeViewModel> InitCreativesBySearch(ICollection<Creative> list);
        Creative InitCreative(CreativeViewModel creativemodel, ApplicationUser user);
        List<CreativeViewModel> GetCreativesRange(int start, int count, int sortType);
        void SetRatingToCreative(int rating, CreativeViewModel creativemodel, ApplicationUser user);
        List<CreativeViewModel> GetPopularCreatives();
        ICollection<ChapterViewModel> InitChapters(ICollection<Chapter> list);
        ICollection<ChapterViewModel> InitChaptersBySearch(ICollection<Chapter> list);
        ICollection<Tag> GetTags(String tagList);
        ICollection<Tag> GetTags(String tagList, Chapter chapter);
        ICollection<Chapter> InitChapters(ICollection<ChapterViewModel> list);
        void CalculateRating(Rating rating, Creative creative);

    }
}