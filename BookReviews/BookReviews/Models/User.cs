using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BookReviews.Models
{
    public class AppUser : IdentityUser
    {
        [StringLength(100, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }
    }
}
