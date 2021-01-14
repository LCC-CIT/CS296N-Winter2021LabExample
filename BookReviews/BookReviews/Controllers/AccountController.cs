using Microsoft.AspNetCore.Mvc;


namespace BookReviews.Controllers
{
    public class AccountController : Controller
    {

        public IActionResult Register()
        {
            return View();
        }

    }
}
