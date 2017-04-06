using SellTables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SellTables.Interfaces
{
    public interface IUserService
    {
        List<ApplicationUser> GetAllUsers();
        ApplicationUser GetUserByName(string name);
        ApplicationUser GetCurrentUser(string name);
        bool IsCurrentUserIsAnAdmin(string userId);
        void DeleteUser(string name);
        string GetUserAvatarUri(string userId);
        void BanUser(string userName);
        void UnbanUser(string userName);
        ICollection<Medal> GetAllUserMedals(ApplicationUser user);
        void UpdateUserAvatar(string uri, string userName);
        ICollection<Rating> GetAllUserRatings(ApplicationUser user);
        ICollection<Creative> GetAllUserCreatives(ApplicationUser user);
    }
}