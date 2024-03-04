using Domain.Entities.Master;
using Domain.Entities.Users;
using Domain.Repositories.Base;
using Domain.Repositories.UserModule;
using Domain.RequestFeatured;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.UserModule
{
    public class UserRepository : RepositoryBase<User>, IRepositoryUser
    {
        private readonly SmartDriveContext _context;
        public UserRepository(SmartDriveContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public void CreateEntity(User entity)
        {
            Create(entity);
        }

        public void DeleteEntity(User entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<User>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges)
                .Include(v => v.UserRoles)
                .Include(v => v.UserPhones)
                .Include(v => v.UserAddresses)
                .OrderBy(v => v.UserEntityid).ToListAsync();
        }

        public async Task<User> GetEntityById(int id, bool trackChanges)
        {
            return await GetByCondition(v => v.UserEntityid == id, trackChanges)
                .Include(v => v.UserRoles)
                .Include(v => v.UserPhones)
                .Include(v => v.UserAddresses)
                .SingleOrDefaultAsync();
        }

        public async Task<User> GetUserByUsername(string username, bool trackChanges)
        {
            return await GetByCondition(v => v.UserName== username, trackChanges)
                .Include(v => v.UserRoles)
                .Include(v => v.UserPhones)
                .Include(v => v.UserAddresses)
                .SingleOrDefaultAsync();
        }

        public async Task<User> GetUserByEmail(string email, bool trackChanges)
        {
            return await GetByCondition(v => v.UserEmail == email, trackChanges)
                .Include(v => v.UserRoles)
                .Include(v => v.UserPhones)
                .Include(v => v.UserAddresses)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsersPaging(EntityParameter entityParams, bool trackChanges)
        {
            var users = GetAll(trackChanges)
                .Include(v => v.UserRoles)
                .Include(v => v.UserPhones)
                .Include(v => v.UserAddresses).AsQueryable();

            if (entityParams != null && !string.IsNullOrWhiteSpace(entityParams.SearchBy))
            {
                users = users.Where(u => EF.Functions.Like(u.UserName, $"%{entityParams.SearchBy}%"));
            }

            return PagedList<User>.ToPagedList(users, entityParams.PageNumber, entityParams.PageSize);
        }

        public async Task<User> GetUserByNationalId(string nationalId, bool trackChanges)
        {
            return await GetByCondition(v => v.UserNationalId == nationalId, trackChanges)
                .Include(v => v.UserRoles)
                .Include(v => v.UserPhones)
                .Include(v => v.UserAddresses)
                .SingleOrDefaultAsync();
        }

        public async Task<User> GetUserByNpwp(string npwp, bool trackChanges)
        {
            return await GetByCondition(v => v.UserNpwp == npwp, trackChanges)
                .Include(v => v.UserRoles)
                .Include(v => v.UserPhones)
                .Include(v => v.UserAddresses)
                .SingleOrDefaultAsync();
        }
    }
}
