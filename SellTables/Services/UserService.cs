using SellTables.Interfaces;
using SellTables.Lucene;
using SellTables.Models;
using SellTables.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SellTables.Services
{
    public class UserService
    {
        private IUserRepository UsersRepository;
        private IRepository<Rating> RatingsRepository;
        private IRepository<Creative> CreativesRepository;
        private ApplicationDbContext DataBaseContext;
        private CreativeService CreativeService;

        public UserService(ApplicationDbContext DataBaseContext)
        {
            this.DataBaseContext = DataBaseContext;
            UsersRepository = new UsersRepository(DataBaseContext);
            CreativeService = new CreativeService(DataBaseContext);
            CreativesRepository = new CreativesRepository(DataBaseContext);
        }

        internal List<ApplicationUser> GetAllUsers()
        {
            var listOfUsers = UsersRepository.GetAllUsers();
            return listOfUsers.ToList();
        }

        internal ApplicationUser GetUserByName(string name) {
            return UsersRepository.FindUser(name);
        }

        internal ApplicationUser GetCurrentUser(string name) {
            return UsersRepository.GetCurrentUser(name);
        }

        public void DeleteUser(string name) {
            ApplicationUser user = GetUserByName(name);
            DeleteAllUsersRatings(user);
            DeleteAllUsersCreatives(user);
            ReCalculateRating();
            DeleteUser(user);
        }


        private void ReCalculateRating() {
            var creativesList = CreativeService.GetAllCreativesModels();
            
            foreach (var creative in creativesList)
            {
                double a = 0;
                foreach (var r in creative.Ratings)
                {
                    a += r.Value;
                }
                if (creative.Ratings.Count != 0)
                    a /= creative.Ratings.Count;
                else a /= 1;

                creative.Rating = Math.Round(a, 2);
              CreativesRepository.Update(creative);
            }
        }

        private void DeleteUser(ApplicationUser user) {
           
     var u1 = UsersRepository.FindUser(user.UserName);
            if(u1!=null)
 UsersRepository.DeleteUser(u1);   
        }


       private void DeleteAllUsersCreatives(ApplicationUser user)
        {
            var creatives = GetAllUserCreatives(user);
            DeleteAllChaptersFromCreatives(creatives);
            if (creatives != null)
            {
                DataBaseContext.Creatives.RemoveRange(creatives);
                foreach (var c in creatives) {
                    CreativeSearch.ClearLuceneIndexRecord(c.Id);
                }
            }
            DataBaseContext.SaveChanges(); // External login can't be deleted :O
        }

        private void DeleteAllChaptersFromCreatives(ICollection<Creative> creatives) {
            foreach (var creative in creatives) {
                foreach (var chapter in creative.Chapters.ToList()) {
                    foreach(var tag in chapter.Tags.ToList()){
                        var t = DataBaseContext.Tags.Find(tag.Id);
                        if(t!=null)
                        DataBaseContext.Tags.Remove(t);
                        DataBaseContext.SaveChanges();
                    }   
                    var c = DataBaseContext.Chapters.Find(chapter.Id);
                    if (c != null)
                    {
                        DataBaseContext.Chapters.Remove(c);
                        DataBaseContext.SaveChanges();
                    }
                }
            }
        }



       private void DeleteAllUsersRatings(ApplicationUser user)
        {
            var ratings = GetAllUserRatings(user);
            if (ratings != null)
                DataBaseContext.Rating.RemoveRange(ratings);
            DataBaseContext.SaveChanges();
        }

        public ICollection<Rating> GetAllUserRatings(ApplicationUser user)
        {
            if (user==null)
                return null;
            return DataBaseContext.Rating
                        .Where(r => r.UserId == user.Id).ToList();
        }

        public ICollection<Creative> GetAllUserCreatives(ApplicationUser user)
        {
            if (user == null)
                return null;
            return DataBaseContext.Creatives
                        .Where(r => r.UserId == user.Id).ToList();
        }


    }
}