using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookReviews.Models
{
    public class SeedData
    {
        public static void Seed(BookReviewContext context)
        {
          if (!context.Reviews.Any())  // this is to prevent duplicate data from being added
            {
                Review review = new Review
                {
                    BookTitle = "Prince of Foxes",
                    AuthorName = "Samuel Shellabarger",
                    ReviewText = "Great book, a must read!",
                    Reviewer = new User { Name = "Emma Watson" },
                    ReviewDate = DateTime.Parse("11/1/2020")
                };
                context.Reviews.Add(review);  // queues up the review to be added to the DB

                review = new Review
                {
                    BookTitle = "Prince of Foxes",
                    AuthorName = "Samuel Shellabarger",
                    ReviewText = "I love the clever, witty dialog",
                    Reviewer = new User { Name = "Daniel Radliiffe" },
                    ReviewDate = DateTime.Parse("11/15/2020")
                };
                context.Reviews.Add(review);

                // My next two reviews will be by the same user, so I will create
                // the user object once and store it so that both reviews will be
                // associated with the same entity in the DB.

                User reviewerBrianBird = new User() { Name = "Brian Bird" };
                context.Users.Add(reviewerBrianBird);
                context.SaveChanges();   // This will add a UserID to the reviewer object

                review = new Review
                {
                    BookTitle = "Virgil Wander",
                    AuthorName = "Lief Enger",
                    ReviewText = "Wonderful book, written by a distant cousin of mine.",
                    Reviewer = reviewerBrianBird,
                    ReviewDate = DateTime.Parse("11/30/2020")
                };
                context.Reviews.Add(review);

                review = new Review
                {
                    BookTitle = "Ivanho",
                    AuthorName = "Sir Walter Scott",
                    ReviewText = "It was a little hard going at first, but then I loved it!",
                    Reviewer = reviewerBrianBird,
                    ReviewDate = DateTime.Parse("11/1/2020")
                };
                context.Reviews.Add(review);


                context.SaveChanges(); // stores all the reviews in the DB
            }
        }
    }
}
