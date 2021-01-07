using System;
using System.ComponentModel.DataAnnotations;

namespace BookReviews.Models
{
    public class Review
    {
        public int ReviewID { get; set; }
        [Required (ErrorMessage = "Title is required")]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "Title must be between 3 and 100 characters")]
        public string BookTitle { get; set; }
        [Required(ErrorMessage = "Author is required")]
        [StringLength(100, MinimumLength = 3)]
        public string AuthorName { get; set; }
        public User Reviewer { get; set; }
        [Required]
        public string ReviewText { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
