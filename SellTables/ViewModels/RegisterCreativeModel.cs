using SellTables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SellTables.ViewModels
{
    public class RegisterCreativeModel
    {
        public Creative Creative { get; set; }
        public ICollection<Chapter> Chapters { get; set; }
        public Chapter Chapter { get; set; }

        public RegisterCreativeModel() {
            Chapters = new List<Chapter>();
        }
    }
}