using MarketPlace.Core.Constants;
using MarketPlace.Infrastructure.Constants;
using System.ComponentModel.DataAnnotations;

namespace MarketPlace.Core.Models.ProductDto
{
    public class ProductFormModel
    {
        [Required(ErrorMessage = MessageConstants.RequiredMessage)]
        [StringLength(DataConstants.ProductNameMaxLength, MinimumLength = DataConstants.ProductNameMinLength, ErrorMessage = MessageConstants.LengthMessage)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = MessageConstants.RequiredMessage)]
        [StringLength(DataConstants.ProductDescriptionMaxLength, MinimumLength = DataConstants.ProductDescriptionMinLength, ErrorMessage = MessageConstants.LengthMessage)]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = MessageConstants.RequiredMessage)]
        [Range(typeof(decimal), DataConstants.ProductPriceMinimum, DataConstants.ProductPriceMaximum)]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = MessageConstants.RequiredMessage)]
        public string Image { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public string Seller { get; set; } = null!;

        [Display(Name = "Category")]
        public int CategoryId{ get; set; }

        public IEnumerable<ProductCategoryServiceModel> Categories { get; set; } = new List<ProductCategoryServiceModel>();
    }
}
