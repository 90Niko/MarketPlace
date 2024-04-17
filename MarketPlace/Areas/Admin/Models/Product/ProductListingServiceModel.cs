namespace MarketPlace.Areas.Admin.Models.Product
{
    public class ProductListingServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; } 

        public string ImageUrl { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public string CreatedOn { get; set; } = string.Empty;

        public string Seller { get; set; } = string.Empty;

        public bool IsApproved { get; set; }
    }
}
