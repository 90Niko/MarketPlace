using MarketPlace.Infrastructure.Constants;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MarketPlace.Infrastructure.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(DataConstants.FirstNameMaxLength)]
        [PersonalData]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(DataConstants.LastNameMaxLength)]
        [PersonalData]
        public string LastName { get; set; } = string.Empty;
    }
}
