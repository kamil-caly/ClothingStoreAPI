namespace ClothingStoreAPI.Entities
{
    public class Basket
    {
        public int Id { get; set; }
        public virtual User CreatedBy { get; set; }
        public int? CreatedById { get; set; }
        public virtual List<Order> Orders { get; set; }

    }
}
