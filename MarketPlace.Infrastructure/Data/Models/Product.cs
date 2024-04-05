using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static MarketPlace.Infrastructure.Constants.DataConstants;

namespace MarketPlace.Infrastructure.Data.Models
{
    public class Product
    {
        [Key]
        [Comment("Product identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(ProductNameMaxLength)]
        [Comment("This is the name of the product")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Comment("This is the image of the product")]
        public string Image { get; set; } = string.Empty;

        [Required]
        [MaxLength(ProductDescriptionMaxLength)]
        [Comment("This is the description of the product")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Comment("This is the price of the product")]
        public decimal Price { get; set; }

        [Required]
        [Comment("This is the date the product was created")]
        public DateTime CreatedOn { get; set; }

        public bool IsSold { get; set; } = false;

        public bool IsApproved { get; set; } = false;

        [Required]
        [Comment("This is the quantity of the product")]
        public int Quantity { get; set; }

        public int CartQuantity { get; set; } = 0;

        [Required]
        [Comment("User identifier")]
        public string SellerId { get; set; } = string.Empty;

        [Required]
        [ForeignKey("SellerId")]
        public ApplicationUser Seller { get; set; } = null!;

        [Required]
        [Comment("Product Category identifier")]
        public int CategoryId { get; set; }

        [Required]
        [ForeignKey("CategoryId")]
        public Category Category { get; set; } = null!;

        public ICollection<ProductRating> productRatings { get; set; } = new List<ProductRating>();


        // public int ShipingAddressId { get; set; }
    }
}
