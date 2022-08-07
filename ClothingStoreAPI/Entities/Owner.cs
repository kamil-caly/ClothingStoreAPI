namespace ClothingStoreAPI.Entities
{
    public class Owner
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string ContactEmail { get; set; }
        public string ContactNumber { get; set; }
        public virtual ClothingStore Store { get; set; }

    }
}