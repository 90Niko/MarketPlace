using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static MarketPlace.Infrastructure.Constants.DataConstants;

namespace MarketPlace.Infrastructure.Data.Models
{
    [Comment("This is the category of the product")]
    public class Category
    {
        [Key]
        [Comment("This is the primary key of the category")]
        public int Id { get; set; }

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        [Comment("This is the name of the category")]
        public string Name { get; set; }=string.Empty;

    }
}