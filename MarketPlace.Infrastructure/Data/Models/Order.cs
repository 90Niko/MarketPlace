using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Infrastructure.Data.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string BuyerId { get; set; } = string.Empty;

        [Required]
        public string SellerId { get; set; } = string.Empty;

        [Required]
        public DateTime OrderDate { get; set; }
 
        public string ShipingAddress { get; set; } = string.Empty;

        public string ProductName { get; set; } = string.Empty;
    }
}
