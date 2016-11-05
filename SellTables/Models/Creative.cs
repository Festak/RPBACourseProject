using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SellTables.Models
{
    public class Creative
    {
        [Key]
        public int Id { get; set; }
        public virtual ICollection<Chapter> Chapters { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public double Rating { get; set; }
       
        public virtual string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public Creative() {
            Chapters = new List<Chapter>();
            Ratings = new List<Rating>();
        }


    }
}