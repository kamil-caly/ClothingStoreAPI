using System.ComponentModel.DataAnnotations;

namespace ClothingStoreAPI.Entities
{
    public class StoreReview
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        [Range(1,5)]
        public int Rating { get; set; }
        public virtual ClothingStore Store { get; set; }
        public int StoreId { get; set; }
    }
}