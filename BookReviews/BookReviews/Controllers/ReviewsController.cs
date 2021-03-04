using BookReviews.Models;
using BookReviews.Repos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookReviews.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        IReviewRepository repo;

        public ReviewsController(IReviewRepository r)
        {
            repo = r;
        }

        // GET: api/Reviews
        [HttpGet]
        public IActionResult Get()
        {
            var reviews = repo.Reviews.ToList<Review>();
            if (reviews.Count == 0)
            {
                return NotFound();  // return status 404 if there are no reviews
            }
            else
            {
                return Ok(reviews);  // status 200
            }
        }

        // GET api/Reviews/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var review = repo.GetReviewById(id);
            if (review == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(review);
            }
        }

        // POST api/<ReviewsController>
        [HttpPost]
        public IActionResult Post([FromBody] ReviewVM reviewVM)
        {
            if (reviewVM != null)
            {
                var review = new Review
                {
                    BookTitle = reviewVM.BookTitle,
                    AuthorName = reviewVM.AuthorName,
                    ReviewText = reviewVM.ReviewText,
                    Reviewer = new AppUser { Name = reviewVM.Reviewer },
                    ReviewDate = DateTime.Now
                };
                repo.AddReview(review);
                return Ok(review);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPatch("{id}")]
        public IActionResult Patch (int id, string commenter, [FromBody] Comment comment)
        {
            if (id != 0 && commenter != null && comment != null)
            {
                var review = repo.GetReviewById(id);
                comment.Commenter = new AppUser { Name = commenter };
                comment.CommentDate = DateTime.Now;
                review.Comments.Add(comment);
                repo.UpdateReview(review);
                return Ok(review);
            }
            else
            {
                return BadRequest();
            }
            
        }
    }
}
