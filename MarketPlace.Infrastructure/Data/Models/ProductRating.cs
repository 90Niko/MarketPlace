using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MarketPlace.Infrastructure.Constants.DataConstants;

namespace MarketPlace.Infrastructure.Data.Models
{
    public class ProductRating
    {
        [Key]
        [Comment("Product rating identifier")]
        public int Id { get; set; }

        [Required]
        [Range(1, 5)]
        [Column(TypeName = "decimal(18,2)")]
        [Comment("This is the rating of the product")]
        public double Rating { get; set; }

        [Required]
        [StringLength(ProductCommentMaxLength)]
        [Comment("This is the comment of the product")]
        public string Comment { get; set; } = string.Empty;

        [Required]
        [Comment("This is the date the product was rated")]
        public DateTime CreatedAt { get; set; }

        [Required]
        [Comment("User who submitted the rating")]
        public string UserId { get; set; } = string.Empty;

    }
}
