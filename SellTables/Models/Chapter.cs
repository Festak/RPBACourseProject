
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SellTables.Models
{
    public class Chapter
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(20)]
        public string Name { get; set; }
        public int Number { get; set; }
        [AllowHtml]
        public string Text { get; set; }

        public int? CreativeId { get; set; }
        public virtual Creative Creative { get; set; }

        public string TagsString { get; set; }

        public ICollection<Tag> Tags { get; set; }
        public Chapter()
        {
            Tags = new List<Tag>();
        }

    }
}