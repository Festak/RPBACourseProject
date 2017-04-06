using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SellTables.Models
{
    public class Subscribe
    {
        [Key]
        public int Id { get; set; }

        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public string UserEmail { get; set; }

        public virtual DateTime EditDate { get; set; }

        public Subscribe(){
            EditDate = DateTime.Now;
            }
    }
}