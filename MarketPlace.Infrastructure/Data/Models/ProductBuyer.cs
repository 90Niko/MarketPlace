using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketPlace.Infrastructure.Data.Models
{
    [Comment("This is the product buyer")]
    public class ProductBuyer
    {
        [Key]
        [Comment("Buyer identifier")]
        public string BuyerId { get; set; } = null!;

        [ForeignKey("BuyerId")]
        public IdentityUser Buyer { get; set; } = null!;

        [Key]
        [Comment("Product identifier")]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; } = null!;
    }
}
