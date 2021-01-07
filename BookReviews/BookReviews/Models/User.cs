using System.ComponentModel.DataAnnotations;

namespace BookReviews.Models
{
    public class User
    {
        public int UserID { get; set; }
        [StringLength(100, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }
    }
}
