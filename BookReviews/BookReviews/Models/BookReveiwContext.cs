using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;



namespace BookReviews.Models
{
    public class BookReviewContext : IdentityDbContext
    {
        public BookReviewContext(

     DbContextOptions<BookReviewContext> options) : base(options) { }

        public DbSet<Review> Reviews { get; set; }
        public DbSet<AppUser> Users { get; set; }
    }
}
