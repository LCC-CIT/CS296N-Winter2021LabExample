using BookReviews.Models;
using BookReviews.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookReviews.Views.Book
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

        [HttpPost]
        public IActionResult Post([FromBody] CommentVM commentVM)
        {
            Comment comment;
            if (commentVM == null)
            {
                return BadRequest();
            }
            else
            {
                comment = new Comment
                {
                    CommentText = commentVM.CommentText,
                    Commenter = new AppUser { Name = commentVM.CommenterName }
                };
                comment.CommentDate = DateTime.Now;


                // Retrieve the review that this comment is for
                var review = repo.GetReviewById(commentVM.ReviewID);
                if (review != null)
                {
                    // Store the review with the comment in the database
                    review.Comments.Add(comment);
                    repo.UpdateReview(review);
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
        }
    }
}
