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

        public void UpdateReview(Review review)
        {
            throw new NotImplementedException();
        }
    }
}
