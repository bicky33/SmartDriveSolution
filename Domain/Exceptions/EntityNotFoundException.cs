namespace Domain.Exceptions
{
    public class EntityNotFoundException : NotFoundException
    {
        public EntityNotFoundException(int? id, string message) : base($"Entity {message} with identifier {id} not found")
        {
        }

        public EntityNotFoundException(string? id, string message) : base($"Entity {message} with identifier {id} not found")
        {
        }
    }
}