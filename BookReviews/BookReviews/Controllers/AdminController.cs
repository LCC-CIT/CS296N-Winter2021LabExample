using BookReviews.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookReviews.Controllers
{
    public class AdminController : Controller
    {

        private UserManager<AppUser> userManager; 
        private RoleManager<IdentityRole> roleManager;

        public AdminController(UserManager<AppUser> userMngr, 
                               RoleManager<IdentityRole> roleMngr)
        {
            userManager = userMngr; roleManager = roleMngr;
        }

        public async Task<IActionResult> Index()
        {
            List<AppUser> users = new List<AppUser>(); 
            foreach (AppUser user in userManager.Users)
            {
                user.RoleNames = await userManager.GetRolesAsync(user); 
                users.Add(user);
            }

            AdminVM model = new AdminVM  {
                Users = users, // List of AppUsers
                Roles = roleManager.Roles }; 
            
            return View(model);
        } // the other action methods } 
    }
}
