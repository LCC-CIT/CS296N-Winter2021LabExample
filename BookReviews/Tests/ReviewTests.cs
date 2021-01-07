using BookReviews.Controllers;
using BookReviews.Models;
using BookReviews.Repos;
using System.Linq;
using Xunit;

namespace Tests
{
    public class ReviewTests
    {
        [Fact]
        public void AddReviewTest()
        {
            // Arrange
            var fakeRepo = new FakeReviewRepository();
            var controller = new BookController(fakeRepo);
            var review = new Review()
            {
                BookTitle = "Grapes of Wrath",
                AuthorName = "Salinger",
                Reviewer = new User() { Name = "Me" },
                ReviewText = "Never actually read it"
            };

            // Act
            controller.Review(review);

            // Assert
            // Ensure that the review was added to the repository
            var retrievedReview = fakeRepo.Reviews.ToList()[0];
            Assert.Equal(System.DateTime.Now.Date.CompareTo(retrievedReview.ReviewDate.Date), 0);
        }
    }
}
