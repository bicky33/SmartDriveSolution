namespace Domain.Exceptions
{
    public abstract class AlreadyExistException : Exception
    {
        public AlreadyExistException(string? message) : base(message)
        {
        }
    }
}