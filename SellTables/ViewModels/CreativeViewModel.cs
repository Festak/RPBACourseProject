using SellTables.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SellTables.Models
{
    public class CreativeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Rating { get; set; }
        public string CreationDate { get; set; }
        public string UserName { get; set; }
        public ICollection<ChapterViewModel> Chapters { get; set; }

        public CreativeViewModel()
        {
            Chapters = new List<ChapterViewModel>();
        }


    }
}