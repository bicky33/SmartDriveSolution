namespace Domain.Exceptions
{
    public class EntityAlreadyExistException : AlreadyExistException
    {
        public EntityAlreadyExistException(int? id, string message) : base($"Entity {message} with identifier {id} not found")
        {
        }
        public EntityAlreadyExistException(string? id, string message) : base($"Entity {message} with identifier {id} Already Exist")
        {
        }
    }
}