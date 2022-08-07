namespace ClothingStoreAPI.Entities
{
    public class ClothingStore
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ContactNumber { get; set; }
        public string ContactEmail { get; set; }
        public string Headquaters { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal? Incame { get; set; }
        public virtual Address Address { get; set; }
        public int AddressId { get; set; }
        public virtual Owner Owner { get; set; }
        public int OwnerId { get; set; }
        public virtual List<StoreReview> StoreReviews { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}
