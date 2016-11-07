using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SellTables.Models
{
    public class Chapter
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(20)]
        public string Name { get; set; }
        public int Number { get; set; }
        public string Text { get; set; }
        public bool IsReading { get; set; }

        public int CreativeId { get; set; }
     //   public Creative Creative { get; set; }


        public ICollection<Tag> Tags { get; set; }
        public Chapter() {
            Tags = new List<Tag>();
        }

    }
}