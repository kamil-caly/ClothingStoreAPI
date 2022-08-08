namespace ClothingStoreAPI.Exceptions
{
    public class NotFoundAnyStoresException : Exception
    {
        public NotFoundAnyStoresException(string message)
            : base(message)
        {

        }
    }
}
