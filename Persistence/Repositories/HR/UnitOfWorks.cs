using Domain.Repositories.Base;

namespace Persistence.Repositories.HR
{
    public class UnitOfWorks : IUnitOfWorks
    {
        private readonly SmartDriveContext _dbContext;

        public UnitOfWorks(SmartDriveContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;

            }
           // return await _dbContext.SaveChangesAsync();
        }
        
    }
}
