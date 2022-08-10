namespace ClothingStoreAPI.Exceptions
{
    public class SoldOutException : Exception
    {
        public SoldOutException(string message) : base(message)
        {

        }
    }
}
