using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SellTables.ViewModels
{
    public class TagViewModel
    {
        public int Id { get; set; }
        [MaxLength(13)]
        public string Description { get; set; }
    }
}