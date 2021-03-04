using BookReviews.Models;
using BookReviews.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        [HttpGet]
        public IActionResult Get()
        {
            var reviews = repo.Reviews.ToList<Review>();
            if (reviews.Count == 0)
            {
                return NotFound();  // status code 404
            }
            else
            {
                return Ok(reviews);    // status code 200
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]ReviewVM reviewVM)
        {
            if (reviewVM != null)
            {
                var review = new Review
                {
                    BookTitle = reviewVM.BookTitle,
                    AuthorName = reviewVM.Author,
                    ReviewText = reviewVM.ReviewText,
                    Reviewer = new AppUser { Name = reviewVM.ReviewerName },
                    ReviewDate = DateTime.Now
                };

                repo.AddReview(review);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
