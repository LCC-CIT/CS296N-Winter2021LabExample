using System;
using System.Collections.Generic;
using System.Linq;
using BookReviews.Models;
using BookReviews.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookReviews.Controllers
{
    public class BookController : Controller
    {
        IReviewRepository repo;
        UserManager<AppUser> userManager;

        public BookController(IReviewRepository r, UserManager<AppUser> u)
        {
            repo = r;
            userManager = u;
        }


        public IActionResult Index()
        {
            
            return View();
        }

        // Show the view that contains a form for entering a review
        [Authorize]
        public IActionResult Review()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Review(Review model)
        {
            model.Reviewer = userManager.GetUserAsync(User).Result;
            // TODO: get the user's real name in registration
            model.Reviewer.Name = model.Reviewer.UserName;  // temporary hack
            model.ReviewDate = DateTime.Now;
            // Store the model in the database
            repo.AddReview(model);

            return View(model);
        }

        public IActionResult Reviews()
        {
            // Get all reviews in the database
            List<Review> reviews = repo.Reviews.ToList<Review>(); // Use ToList to convert the IQueryable to a list
            // var reviews = context.Reviews.Include(book => book.Reviewer).ToList<Review>();
            return View(reviews);
        }

        [HttpPost]
        public IActionResult Reviews(string bookTitle, string reviewerName)
        {
            List<Review> reviews = null;

            if (bookTitle != null)
            {
               reviews = (from r in repo.Reviews
                               where r.BookTitle == bookTitle
                               select r).ToList();
            }
            else if (reviewerName != null)
            {
                reviews = (from r in repo.Reviews
                           where r.Reviewer.Name == reviewerName
                           select r).ToList();
            }

            return View(reviews);
        }

        // Open the form for entering a comment
        [Authorize]
        public IActionResult Comment(int reviewId)
        {
            var commentVM = new CommentVM { ReviewID = reviewId };
            return View(commentVM);
        }

        [HttpPost]
        public RedirectToActionResult Comment(CommentVM commentVM)
        {
            // Comment is the domain model
            var comment = new Comment { CommentText = commentVM.CommentText };
            comment.Commenter = userManager.GetUserAsync(User).Result;
            comment.CommentDate = DateTime.Now;

            // Retrieve the review that this comment is for
            var review = repo.GetReviewById(commentVM.ReviewID);

            // Store the review with the comment in the database
            review.Comments.Add(comment);
            repo.UpdateReview(review);

            return RedirectToAction("Reviews");
        }

    }
};
