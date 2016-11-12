using SellTables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SellTables.ViewModels
{
    public class LoginRegisterUserModel
    {
        public RegisterViewModel RegisterViewModel { get; set; }
        public LoginViewModel LoginViewModel { get; set; }
    }
}