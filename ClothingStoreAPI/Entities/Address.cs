namespace ClothingStoreAPI.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string ApartamentNumber { get; set; }
        public virtual ClothingStore Store { get; set; }

    }
}