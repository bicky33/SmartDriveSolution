namespace Domain.Exceptions
{
    public class EntityNotFoundException : NotFoundExeption
    {
        public EntityNotFoundException(int id) : base($"Entity with identifier {id} not found")
        {
        }
    }
}