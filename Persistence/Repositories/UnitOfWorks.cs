using Domain.Repositories.Base;

namespace Persistence.Repositories
{
    public class UnitOfWorks : IUnitOfWorks
    {
        private readonly SmartDriveContext _context;

        public UnitOfWorks(SmartDriveContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        //using lambda expression
        /*public async Task<int> SaveChangesAsync() => _context.SaveChangesAsync();*/
    }
}