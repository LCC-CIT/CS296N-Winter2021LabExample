using BookReviews.Models;
using BookReviews.Repos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BookReviews.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        IReviewRepository repo;

        public CommentsController(IReviewRepository r)
        {
            repo = r;
        }

        // POST api/<Comments>
        [HttpPost]
        public IActionResult Post([FromBody] CommentVM commentVM)
        {
            var comment = new Comment { CommentText = commentVM.CommentText, 
                Commenter = new AppUser { Name = commentVM.UserName }
            };

            comment.CommentDate = DateTime.Now;
            // Retrieve the review that this comment is for
            var review = (from r in repo.Reviews
                          where r.ReviewID == commentVM.ReviewID
                          select r).FirstOrDefault<Review>();
            if (review != null)
            {
                // Store the review with the comment in the database
                review.Comments.Add(comment);
                repo.UpdateReview(review);
                return Ok(review);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
