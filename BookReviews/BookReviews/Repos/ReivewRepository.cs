using BookReviews.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BookReviews.Repos
{
    public class ReviewRepository : IReviewRepository
    {
        private BookReviewContext context;

        // constructor
        public ReviewRepository(BookReviewContext c)
        {
            context = c;
        }

        public IQueryable<Review> Reviews 
        { 
            get 
            {
                // Get all the Review objects in the Reviews DbSet
                // and include the Reivewer object and list of comments in each Review.
                return context.Reviews.Include(review => review.Reviewer)
                                        .Include(review => review.Comments)
                                        .ThenInclude(comment => comment.Commenter);
            }
        }

        public Review GetReviewById(int id)
        {
            return Reviews.SingleOrDefault<Review>(r => r.ReviewID == id);
        }

        public void AddReview(Review review)
        {
            context.Reviews.Add(review);
            context.SaveChanges();
        }

        public void UpdateReview(Review review)
        {
            context.Reviews.Update(review);   // Find the review by ReviewID and update it
            context.SaveChanges();
        }

    }
}
