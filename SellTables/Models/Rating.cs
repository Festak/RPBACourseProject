
using System.ComponentModel.DataAnnotations;


namespace SellTables.Models
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }

        public int Value { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int? CreativeId { get; set; }

        public virtual Creative Creative { get; set; }
    }
}