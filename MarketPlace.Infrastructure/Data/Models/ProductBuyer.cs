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
        public ApplicationUser Buyer { get; set; } = null!;

        [Key]
        [Comment("Product identifier")]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; } = null!;

        [Key]
        [Comment("Shiping address identifier")]
        public int ShipingAddressId { get; set; }

        [ForeignKey("ShipingAddressId")]
        public ShipingAddress ShipingAddress { get; set; } = null!;

        public DateTime BuyAt { get; set; }
    }
}
