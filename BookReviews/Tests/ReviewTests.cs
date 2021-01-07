using BookReviews.Controllers;
using BookReviews.Models;
using BookReviews.Repos;
using System.Linq;
using Xunit;

namespace Tests
{
    public class ReviewTests
    {
        FakeReviewRepository fakeRepo;
        BookController controller;

        // Setup for all tests. Executed for each test.
        public ReviewTests()
        {
            fakeRepo = new FakeReviewRepository();
            controller = new BookController(fakeRepo);
        }

        [Fact]
        public void AddReviewTest()
        {
            // Arrange
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
            // Ensure that the review was added to the repository with the correct date
            var retrievedReview = fakeRepo.Reviews.ToList()[0];
            Assert.Equal(0, System.DateTime.Now.Date.CompareTo(retrievedReview.ReviewDate.Date));
        }

        [Fact]
        public void AddReviewValidationTest()
        {
            // Arrange
            var review = new Review()
            {
                BookTitle = "1234567890123456789012345678901234567890" +
                "1234567890123456789012345678901234567890" +
                "123456789012345678901",  // too long
                AuthorName = null,  // required but null
                Reviewer = new User() { Name = "Me" },
                ReviewText = ""   // too short
            };

            // Act
            controller.Review(review);

            // Assert
            // Ensure that the review was not added to the repository
            Assert.Empty(fakeRepo.Reviews.ToList());
        }
    }
}
