using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace SellTables.Models
{
    public class Creative
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(20)]
        public string Name { get; set; }
        public virtual ICollection<Chapter> Chapters { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public double Rating { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime EditDate { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string CreativeUri { get; set; }

        public Creative()
        {
            CreationDate = DateTime.Now;
            EditDate = DateTime.Now;
            Chapters = new List<Chapter>();
            Ratings = new List<Rating>();
        }


    }
}