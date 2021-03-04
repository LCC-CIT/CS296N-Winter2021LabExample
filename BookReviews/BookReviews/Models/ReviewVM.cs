using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookReviews.Models
{
    public class ReviewVM
    {
        public string BookTitle { get; set; }
        public string Author { get; set; }
        public string ReviewText { get; set; }
        public string ReviewerName { get; set; }
    }
}
