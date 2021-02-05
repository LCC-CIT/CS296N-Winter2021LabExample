using BookReviews.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookReviews.Repos
{
    public interface IReviewRepository
    {
        IQueryable<Review> Reviews { get; }  // Read (or retrieve) reviews
        void AddReview(Review review);  // Create a review
        void UpdateReview(Review review); // modify review
    }
}
