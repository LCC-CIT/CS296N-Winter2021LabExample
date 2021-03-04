using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookReviews.Models
{
    public class CommentVM
    {
        public int ReviewID { get; set; }    // This identifies the reivew being commented on
        public int BookTitle { get; set; }
        public String CommentText { get; set; }
        public String UserName { get; set; }   // only used with unauthenticated users
    }
}
