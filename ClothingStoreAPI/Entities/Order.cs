namespace ClothingStoreAPI.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOrderDate { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductSize { get; set; }
        public string ProductGender { get; set; }
        public bool IsBought { get; set; }
        public virtual Basket Basket { get; set; }
        public int BasektId { get; set; }

    }
}