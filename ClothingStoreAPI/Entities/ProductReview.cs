using System.ComponentModel.DataAnnotations;

namespace ClothingStoreAPI.Entities
{
    public class ProductReview
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        [Range(1,5)]
        public int Rating { get; set; }
        public virtual Product Product { get; set; }
        public int ProductId { get; set; }
    }
}