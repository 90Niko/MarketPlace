using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static MarketPlace.Infrastructure.Constants.DataConstants;

namespace MarketPlace.Infrastructure.Data.Models
{
    [Comment("This is the shipping address")]
    public class ShipingAddress
    {
        [Key]
        [Comment("Shipping address identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(AddressStreetMaxLength)]
        [Comment("This is the street of the shipping address")]
        public string Street { get; set; } = string.Empty;

        [Required]
        [MaxLength(AddressCityMaxLength)]
        [Comment("This is the city of the shipping address")]
        public string City { get; set; } = string.Empty;

        [Required]
        [MaxLength(AddressCountryMaxLength)]
        [Comment("This is the state of the shipping address")]
        public string Country { get; set; } = string.Empty;

        [Required]
        [Comment("This is the zip code of the shipping address")]
        public string ZipCode { get; set; } = string.Empty;

        [Required]
        [Comment("User identifier")]
        public string UserId { get; set; } = string.Empty;

        [ForeignKey("UserId")]
        public IdentityUser User { get; set; } = null!;

        public ICollection<ProductBuyer> ProductsBuyer { get; set; } = null!;
    }
}
