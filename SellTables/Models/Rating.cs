using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SellTables.Models
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }
        [Range(0,5)]
        public int Value { get; set; }

        public int ChapterId { get; set; }
        public Chapter Chapter{ get; set; }
    }
}