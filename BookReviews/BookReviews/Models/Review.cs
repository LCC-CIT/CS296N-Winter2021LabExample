using System;

namespace BookReviews.Models
{
    public class Review
    {
        public int ReviewID { get; set; }
        public string BookTitle { get; set; }
        public string AuthorName { get; set; }
        public User Reviewer { get; set; }
        public string ReviewText { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
