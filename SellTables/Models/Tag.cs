﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SellTables.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Chapter> Chapters { get; set; }
        public Tag() {
            Chapters = new List<Chapter>();
        }

    }
}