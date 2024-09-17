namespace MarketPlace.Areas.Admin.Models.Product
{
    public class CategoryServiceModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public bool IsDeleted { get; set; }
    }
}
