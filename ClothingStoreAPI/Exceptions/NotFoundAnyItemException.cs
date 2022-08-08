namespace ClothingStoreAPI.Exceptions
{
    public class NotFoundAnyItemException : Exception
    {
        public NotFoundAnyItemException(string message)
            : base(message)
        {

        }
    }
}
