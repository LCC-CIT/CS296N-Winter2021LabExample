
using System.ComponentModel.DataAnnotations;

namespace BookReviews.Models
{
    public class User
    {
        public int UserID { get; set; }
        [StringLength(60, MinimumLength = 1)]
        [Required]
        public string Name { get; set; }
    }
}
