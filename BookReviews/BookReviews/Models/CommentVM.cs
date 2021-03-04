using System;

namespace BookReviews.Models
{
    public class CommentVM
    {
        public int ReviewID { get; set; }    // This identifies the reivew being commented on
        public int BookTitle { get; set; }
        public String CommentText { get; set; }
        public String CommenterName { get; set; }  // Only used when the user isn't authenticated
    }
}