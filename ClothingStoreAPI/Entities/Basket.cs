namespace ClothingStoreAPI.Entities
{
    public class Basket
    {
        public int Id { get; set; }
        public virtual List<Order> Orders { get; set; }

    }
}
