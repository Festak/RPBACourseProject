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
        ApplicationUser FindUser(string userName);
     ApplicationUser FindUserById(string userId);
        IdentityResult UpdateUser(ApplicationUser user);
        IdentityResult DeleteUser(ApplicationUser user);
        bool IsInAdminRole(string userId);
        ApplicationUser GetCurrentUser(string id);
    }
}
