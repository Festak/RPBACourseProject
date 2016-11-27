using SellTables.Interfaces;
using SellTables.Lucene;
using SellTables.Models;
using SellTables.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace SellTables.Services
{
    public class UserService : IUserService
    {
        private IUserRepository UsersRepository;
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

        public List<ApplicationUser> GetAllUsers()
        {
            var listOfUsers = UsersRepository.GetAllUsers();
            return listOfUsers.ToList();
        }

        public ApplicationUser GetUserByName(string name)
        {
            return UsersRepository.FindUser(name);
        }

        public ApplicationUser GetCurrentUser(string name)
        {
            return UsersRepository.GetCurrentUser(name);
        }

        public bool IsCurrentUserIsAnAdmin(string userId)
        {
            bool IsCurrentUserIsAnAdmin = UsersRepository.IsInAdminRole(userId);
            return IsCurrentUserIsAnAdmin;

        }

        public void DeleteUser(string name)
        {
            ApplicationUser user = GetUserByName(name);
            DeleteAllUserRatings(user);
            DeleteAllUserCreatives(user);
            ReCalculateRating();
            DeleteUser(user);
        }

        public string GetUserAvatarUri(string userId)
        {
            ApplicationUser user = UsersRepository.FindUserById(userId);
            return user.AvatarUri;
        }

        public void BanUser(string userName)
        {
            ApplicationUser user = UsersRepository.FindUser(userName);
            if (!UsersRepository.IsInAdminRole(user.Id))
            {
                user.LockoutEndDateUtc = DateTime.UtcNow.AddDays(15);
                UsersRepository.UpdateUser(user);
            }
        }

        public void UnbanUser(string userName)
        {
            ApplicationUser user = UsersRepository.FindUser(userName);
            user.LockoutEndDateUtc = null;
            UsersRepository.UpdateUser(user);
        }

        public ICollection<Medal> GetAllUserMedals(ApplicationUser user)
        {
            if (user == null)
                return null;
            return DataBaseContext.Medals.Include(u => u.Users.Where(m => m.Id == user.Id)).ToList();

        }

        public void UpdateUserAvatar(string uri, string userName)
        {
            ApplicationUser user = UsersRepository.FindUser(userName);
            user.AvatarUri = uri;
            UsersRepository.UpdateUser(user);

        }

        public ICollection<Rating> GetAllUserRatings(ApplicationUser user)
        {
            if (user == null)
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

        private void ReCalculateRating()
        {
            var creativesList = CreativeService.GetAllCreativesModels();
            foreach (var creative in creativesList)
            {
                double a = GetAverageValue(creative);
                creative.Rating = Math.Round(a, 2);
                CreativesRepository.Update(creative);
            }
        }

        private double GetAverageValue(Creative creative)
        {
            double average = 0;
            foreach (var r in creative.Ratings)
            {
                average += r.Value;
            }
            average = GetAverageValueForAllRatings(creative, average);
            return average;
        }

        private double GetAverageValueForAllRatings(Creative creative, double average)
        {
            if (creative.Ratings.Count != 0)
                average /= creative.Ratings.Count;
            else average /= 1;
            return average;
        }

        private void DeleteUser(ApplicationUser user)
        {
            var u1 = UsersRepository.FindUser(user.UserName);
            if (u1 != null)
                UsersRepository.DeleteUser(u1);
        }


        private void DeleteAllUserCreatives(ApplicationUser user)
        {
            var creatives = GetAllUserCreatives(user);
            DeleteUserCreativesOneByOne(user, creatives);
            DeleteUserExternalLogins(user);
            DeleteAllChaptersFromCreatives(creatives);
            DeleteCreatives(creatives);
            DataBaseContext.SaveChanges();
        }

        private void DeleteUserCreativesOneByOne(ApplicationUser user, ICollection<Creative> creatives)
        {
            foreach (var cr in creatives)
            {
                user.Creatives.Remove(cr);
            }
        }

        private void DeleteUserExternalLogins(ApplicationUser user)
        {
            foreach (var login in user.Logins.ToList())
            {
                user.Logins.Remove(login);
            }
        }

        private void DeleteCreatives(ICollection<Creative> creatives)
        {
            if (creatives != null)
            {
                foreach (var c in creatives)
                {
                    c.User = null;
                    c.UserId = null;
                    CreativeSearch.ClearLuceneIndexRecord(c.Id);
                }
                DataBaseContext.Creatives.RemoveRange(creatives);
            }
        }


        private void DeleteAllChaptersFromCreatives(ICollection<Creative> creatives)
        {
            foreach (var creative in creatives)
            {
                DeleteChaptersFromCreative(creative);
            }
        }

        private void DeleteChaptersFromCreative(Creative creative)
        {
            foreach (var chapter in creative.Chapters.ToList())
            {
                DeleteTagFromChapter(chapter);
                var c = DataBaseContext.Chapters.Find(chapter.Id);
                if (c != null)
                {
                    DataBaseContext.Chapters.Remove(c);
                    DataBaseContext.SaveChanges();
                }
            }
        }

        private void DeleteTagFromChapter(Chapter chapter)
        {
            foreach (var tag in chapter.Tags.ToList())
            {
                var t = DataBaseContext.Tags.Find(tag.Id);
                if (t != null)
                    DataBaseContext.Tags.Remove(t);
                DataBaseContext.SaveChanges();
            }
        }

        private void DeleteAllUserRatings(ApplicationUser user)
        {
            var ratings = GetAllUserRatings(user);
            if (ratings != null)
            {
                DataBaseContext.Rating.RemoveRange(ratings);
                DataBaseContext.SaveChanges();
            }
        }

        private void DeleteAllUserMedals(ApplicationUser user)
        {
            var medals = GetAllUserMedals(user);
            if (medals != null)
            {
                DataBaseContext.Medals.RemoveRange(medals);
                DataBaseContext.SaveChanges();
            }
        }
    }
}