using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BookReviews.Models
{
    public class AppUser : IdentityUser
    {
        public int UserID { get; set; }
        [StringLength(60, MinimumLength = 1)]
        [Required]
        public string Name { get; set; }
    }
}
