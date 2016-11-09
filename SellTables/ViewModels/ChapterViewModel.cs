using SellTables.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SellTables.ViewModels
{
    public class ChapterViewModel
    {
 
        public int Id { get; set; }
        [MaxLength(20)]
        public string Name { get; set; }
        public int Number { get; set; }
        [AllowHtml]
        public string Text { get; set; }
        public int CreativeId { get; set; }


        public ICollection<Tag> Tags { get; set; }
        public ChapterViewModel()
        {
            Tags = new List<Tag>();
        }
    }
}