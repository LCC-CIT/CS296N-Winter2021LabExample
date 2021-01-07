using System;
using System.ComponentModel.DataAnnotations;

namespace BookReviews.Models
{
    public class Review
    {
        public int ReviewID { get; set; }
        [Required]
        public string BookTitle { get; set; }
        public string AuthorName { get; set; }
        public User Reviewer { get; set; }
        [Required]
        public string ReviewText { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
