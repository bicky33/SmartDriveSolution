namespace Domain.Exceptions
{
    public abstract class NotFoundExeption : Exception
    {
        public NotFoundExeption(string? message) : base(message)
        {
        }
    }
}