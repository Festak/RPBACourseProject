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
        List<CreativeViewModel> GetCreativesRange(int start, int count, int sortType);
        Creative GetCreative(int id);
        List<Creative> GetAllCreativesModels();
        List<CreativeViewModel> GetPopularCreatives();
        List<CreativeViewModel> GetLastEditedCreatives();
        void DeleteChapterById(int id, string userName);
        List<CreativeViewModel> GetCreativesByUser(string userName);
        void DeleteCreativeById(int id, string userName);
        void SetRatingToCreative(int rating, CreativeViewModel creativemodel, ApplicationUser user);
        void AddChapterToCreative(RegisterCreativeModel model);
        void EditCreativeChapter(RegisterCreativeModel model);
        void UpdateCreativeName(int id, string name);
        void UpdateCreativeImage(int id, string path);
        ICollection<Rating> GetAllCreativeRatings(Creative creative);
        Creative GetCreativeById(int creativeId);
    }
}