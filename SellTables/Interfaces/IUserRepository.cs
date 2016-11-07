using Microsoft.AspNet.Identity;
using SellTables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellTables.Interfaces
{
    interface IUserRepository
    {
        ICollection<ApplicationUser> GetAllUsers();
        Task<ApplicationUser> FindUser(string userName);
        Task<ApplicationUser> FindUserById(string userId);
        Task<IdentityResult> UpdateUser(ApplicationUser user);
        Task<IdentityResult> DeleteUser(ApplicationUser user);
        Task<bool> CheckUserRole(string userId);
        ApplicationUser GetCurrentUser(string id);
    }
}
