namespace ClothingStoreAPI.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Gender { get; set; }
        public string Size { get; set; }
        public DateTime? ProductionYear { get; set; }
        public string ProductionCountry { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public virtual User CreatedBy { get; set; }
        public int? CreatedById { get; set; }
        public virtual ClothingStore Store { get; set; }
        public int StoreId { get; set; }
        public virtual List<ProductReview> ProductReviews { get; set; }

    }
}