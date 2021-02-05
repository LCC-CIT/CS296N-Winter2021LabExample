using BookReviews.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookReviews.Repos
{
    public class FakeReviewRepository : IReviewRepository
    {
        List<Review> reviews = new List<Review>();

        public IQueryable<Review> Reviews 
        { 
            get { return reviews.AsQueryable<Review>(); }
        }


        public void AddReview(Review review)
        {
            review.ReviewID = reviews.Count;
            reviews.Add(review);
        }

        public void UpdateReview(Review reviewUpdate)
        {
            // find the old review by ID
            var review = (from r in reviews
                          where r.ReviewID == reviewUpdate.ReviewID
                          select r).First<Review>();
            review = reviewUpdate;  // replace the old review with the new one
        }

    }
}
