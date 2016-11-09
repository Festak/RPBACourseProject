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
     //   [Range(0,5)]
        public int Value { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int? CreativeId { get; set; }
        public virtual Creative Creative { get; set; }
    }
}