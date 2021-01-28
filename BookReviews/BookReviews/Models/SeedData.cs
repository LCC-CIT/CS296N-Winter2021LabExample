using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;

namespace BookReviews.Models
{
    public class SeedData
    {
        public static void Seed(BookReviewContext context, RoleManager<IdentityRole> roleManager)
        {
          if (!context.Reviews.Any())  // this is to prevent duplicate data from being added
            {
                // Create "Member" role
                // TODO: check the result to see if the role was successfully added
                var result = roleManager.CreateAsync(new IdentityRole("Member")).Result;
                result = roleManager.CreateAsync(new IdentityRole("Admin")).Result;

                AppUser emmaWatson = new AppUser { 
                    UserName = "EWatson", Name = "Emma Watson" };
                context.Users.Add(emmaWatson);
                context.SaveChanges();   // This will add a UserID to the reviewer object

                Review review = new Review
                {
                    BookTitle = "Prince of Foxes",
                    AuthorName = "Samuel Shellabarger",
                    ReviewText = "Great book, a must read!",
                    Reviewer = emmaWatson,
                    ReviewDate = DateTime.Parse("11/1/2020")
                };
                context.Reviews.Add(review);  // queues up the review to be added to the DB

                AppUser danielRadcliffe = new AppUser { 
                    UserName="DRadcliffe", Name = "Daniel Radcliffe" };
                context.Users.Add(danielRadcliffe);
                context.SaveChanges();   // This will add a UserID to the reviewer object

                review = new Review
                {
                    BookTitle = "Prince of Foxes",
                    AuthorName = "Samuel Shellabarger",
                    ReviewText = "I love the clever, witty dialog",
                    Reviewer = danielRadcliffe,
                    ReviewDate = DateTime.Parse("11/15/2020")
                };
                context.Reviews.Add(review);

                // My next two reviews will be by the same user, so I will create
                // the user object once and store it so that both reviews will be
                // associated with the same entity in the DB.

                AppUser reviewerBrianBird = new AppUser() { 
                    UserName = "Bbird", Name = "Brian Bird" };
                context.Users.Add(reviewerBrianBird);
                context.SaveChanges();   // This will add a UserID to the reviewer object

                review = new Review
                {
                    BookTitle = "Virgil Wander",
                    AuthorName = "Leif Enger",
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
