using SellTables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SellTables.ViewModels
{
    public class RegisterCreativeModel
    {
        public Creative Creative { get; set; }
        public ICollection<Chapter> Chapters { get; set; }
        public Chapter Chapter { get; set; }
        public int creativeId { get; set; }
        public int chapterId { get; set; }
        [AllowHtml]
        public string ChapterText { get; set; }
        public byte[] Image { get; set; }

        public RegisterCreativeModel() {
            Chapters = new List<Chapter>();
        }
    }
}