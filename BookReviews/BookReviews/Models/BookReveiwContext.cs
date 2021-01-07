using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BookReviews.Models
{
    public class BookReviewContext : DbContext
    {
        public BookReviewContext(

     DbContextOptions<BookReviewContext> options) : base(options) { }

        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
