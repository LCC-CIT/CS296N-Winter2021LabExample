using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookReviews.Models
{
    public class Comment
    {
        [Key]
        public int CommentID { get; set; }
        public String CommentText { get; set; }
        public DateTime CommentDate { get; set; }
        public AppUser Commenter { get; set; }

    }
}
