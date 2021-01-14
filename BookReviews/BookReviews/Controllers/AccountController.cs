using BookReviews.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookReviews.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;

        public AccountController(UserManager<AppUser> userMngr,
            SignInManager<AppUser> signInMngr)
        {
            userManager = userMngr;
            signInManager = signInMngr;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser { UserName = model.Username }; 
                var result = await userManager.CreateAsync(user, model.Password); 
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false); 
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }


    }
}
