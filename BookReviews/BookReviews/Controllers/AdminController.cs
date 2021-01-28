using BookReviews.Models;
using BookReviews.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookReviews.Controllers
{
   [Authorize]
    public class AdminController : Controller
    {

        private UserManager<AppUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        private IReviewRepository reviewRepo;

        public AdminController(UserManager<AppUser> userMngr,
                               RoleManager<IdentityRole> roleMngr,
                               IReviewRepository repo)
        {
            userManager = userMngr; 
            roleManager = roleMngr;
            reviewRepo = repo;
        }

        public async Task<IActionResult> Index()
        {
            List<AppUser> users = new List<AppUser>();
            foreach (AppUser user in userManager.Users)
            {
                user.RoleNames = await userManager.GetRolesAsync(user);
                users.Add(user);
            }

            AdminVM model = new AdminVM
            {
                Users = users, // List of AppUsers
                Roles = roleManager.Roles
            };

            return View(model);
        } // the other action methods } 

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityResult result = null;
            AppUser user = await userManager.FindByIdAsync(id); 
            if (user != null)
            {
                // Check to see if the user has posted a review
                if (0 == (from r in reviewRepo.Reviews
                          where r.Reviewer.Name == user.Name
                          select r).Count<Review>())
                {

                    result = await userManager.DeleteAsync(user);
                }
                else
                {
                    result = IdentityResult.Failed(new IdentityError() 
                    { Description = "User's reviews must be deleted first" });
                }

                if (!result.Succeeded)
                { 
                    // if failed 
                    string errorMessage = "";
                    foreach (IdentityError error in result.Errors)
                    {
                        errorMessage += error.Description + " | ";
                    }
                    TempData["message"] = errorMessage;
                }
                else
                {
                    TempData["message"] = "";  // No errors, clear the message
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddToAdmin(string id)
        {
            IdentityRole adminRole = await roleManager.FindByNameAsync("Admin"); 
            if (adminRole == null)
            {
                TempData["message"] = "Admin role does not exist. " 
                    + "Click 'Create Admin Role' button to create it.";
            }
            else
            {
                AppUser user = await userManager.FindByIdAsync(id); 
                await userManager.AddToRoleAsync(user, adminRole.Name);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromAdmin(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            var result = await userManager.RemoveFromRoleAsync(user, "Admin");
            if (result.Succeeded) { }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id); 
            await roleManager.DeleteAsync(role); 
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateAdminRole()
        {
            await roleManager.CreateAsync(new IdentityRole("Admin")); 
            return RedirectToAction("Index");
        }
    }
}
