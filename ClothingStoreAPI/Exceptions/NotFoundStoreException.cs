namespace ClothingStoreAPI.Exceptions
{
    public class NotFoundStoreException : Exception
    {
        public NotFoundStoreException(string message) 
            : base(message)
        {

        }
    }
}
