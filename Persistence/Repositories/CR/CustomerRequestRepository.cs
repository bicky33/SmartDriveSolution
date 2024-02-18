using Domain.Entities.CR;
using Domain.Entities.Users;
using Domain.Repositories.CR;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.CR
{
    public class CustomerRequestRepository : RepositoryBase<CustomerRequest>, ICustomerRequestRepository
    {
        public CustomerRequestRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(CustomerRequest entity)
        {
            Create(entity);
        }

        public void DeleteEntity(CustomerRequest entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<CustomerRequest>> GetAllByEmployee(string eawgCode, bool trackChanges)
        {
            //return await GetByCondition(x => x.CreqAgenEntity.EawgArwgCode.Equals(eawgCode), trackChanges)
            //    .Include(x => x.CreqCustEntity)
            //    .ThenInclude(c => c.UserPhones)
            //    .Include(x => x.CreqCustEntity)
            //    .ThenInclude(c => c.UserRoles)
            //    .Include(x => x.CustomerInscAsset)
            //    .ToListAsync();

            IQueryable<CustomerRequest> result = _dbContext.CustomerRequests.AsNoTracking()
                .Where(x => x.CreqAgenEntity.EawgArwgCode.Equals(eawgCode))
                .Include(x => x.CreqCustEntity)
                    .ThenInclude(x => x.UserPhones)
                .Include(x => x.CreqCustEntity)
                    .ThenInclude(x => x.UserRoles)
                .Include(x => x.CustomerInscAsset)
            .Select(x => new CustomerRequest
            {
                CreqEntityid = x.CreqEntityid,
                CreqCreateDate = x.CreqCreateDate,
                CreqStatus = x.CreqStatus,
                CreqType = x.CreqType,
                CreqCustEntity = new User
                {
                    UserFullName = x.CreqCustEntity.UserFullName,
                    UserPhones = x.CreqCustEntity.UserPhones.Select(y => new UserPhone
                    {
                        UsphPhoneNumber = y.UsphPhoneNumber
                    }).ToList(),
                    UserRoles = x.CreqCustEntity.UserRoles.Select(z => new UserRole
                    {
                        UsroRoleName = z.UsroRoleName
                    }).ToList()
                },
                CustomerInscAsset = new CustomerInscAsset
                {
                    CiasIntyName = x.CustomerInscAsset.CiasIntyName
                }
            });

            return result;
        }

        public async Task<IEnumerable<CustomerRequest>> GetAllByUserId(int userId, bool trackChanges)
        {
            //return await GetByCondition(x => x.CreqCustEntityid.Equals(userId), trackChanges)
            //    .Include(x => x.CreqCustEntity)
            //    .ThenInclude(c => c.UserPhones)
            //    .Include(x => x.CreqCustEntity)
            //    .ThenInclude(c => c.UserRoles)
            //    .Include(x => x.CustomerInscAsset)
            //    .ToListAsync();

            IQueryable<CustomerRequest> result = _dbContext.CustomerRequests.AsNoTracking()
                .Where(x => x.CreqCustEntityid.Equals(userId))
                .Include(x => x.CreqCustEntity)
                    .ThenInclude(x => x.UserPhones)
                .Include(x => x.CreqCustEntity)
                    .ThenInclude(x => x.UserRoles)
                .Include(x => x.CustomerInscAsset)
            .Select(x => new CustomerRequest
            {
                CreqEntityid = x.CreqEntityid,
                CreqCreateDate = x.CreqCreateDate,
                CreqStatus = x.CreqStatus,
                CreqType = x.CreqType,
                CreqCustEntity = new User
                {
                    UserFullName = x.CreqCustEntity.UserFullName,
                    UserPhones = x.CreqCustEntity.UserPhones.Select(y => new UserPhone
                    {
                        UsphPhoneNumber = y.UsphPhoneNumber
                    }).ToList(),
                    UserRoles = x.CreqCustEntity.UserRoles.Select(z => new UserRole
                    {
                        UsroRoleName = z.UsroRoleName
                    }).ToList()
                },
                CustomerInscAsset = new CustomerInscAsset
                {
                    CiasIntyName = x.CustomerInscAsset.CiasIntyName
                }
            });

            return result;
        }

        public async Task<IEnumerable<CustomerRequest>> GetAllEntity(bool trackChanges)
        {
            //return await GetAll(trackChanges).OrderBy(x => x.CreqEntityid)
            //    .Include(x => x.CreqCustEntity)
            //    .ThenInclude(c => c.UserPhones)
            //    .Include(x => x.CreqCustEntity)
            //    .ThenInclude(c => c.UserRoles)
            //    .Include(x => x.CustomerInscAsset)
            //    .ToListAsync();

            IQueryable<CustomerRequest> result = _dbContext.CustomerRequests.AsNoTracking()
                .Include(x => x.CreqCustEntity)
                    .ThenInclude(x => x.UserPhones)
                .Include(x => x.CreqCustEntity)
                    .ThenInclude(x => x.UserRoles)
                .Include(x => x.CustomerInscAsset)
            .Select(x => new CustomerRequest
            {
                CreqEntityid = x.CreqEntityid,
                CreqCreateDate = x.CreqCreateDate,
                CreqStatus = x.CreqStatus,
                CreqType = x.CreqType,
                CreqCustEntity = new User
                {
                    UserFullName = x.CreqCustEntity.UserFullName,
                    UserPhones = x.CreqCustEntity.UserPhones.Select(y => new UserPhone
                    {
                        UsphPhoneNumber = y.UsphPhoneNumber
                    }).ToList(),
                    UserRoles = x.CreqCustEntity.UserRoles.Select(z => new UserRole
                    {
                        UsroRoleName = z.UsroRoleName
                    }).ToList()
                },
                CustomerInscAsset = new CustomerInscAsset
                {
                    CiasIntyName = x.CustomerInscAsset.CiasIntyName
                }
            });

            return result;
        }

        public async Task<CustomerRequest> GetEntityById(int id, bool trackChanges)
        {
            return await GetByCondition(x => x.CreqEntityid.Equals(id), trackChanges)
                .Include(x => x.CreqCustEntity)
                    .ThenInclude(x => x.UserPhones)
                .Include(x => x.CreqCustEntity)
                    .ThenInclude(x => x.UserRoles)
                .Include(x => x.CustomerInscAsset).SingleOrDefaultAsync();
        }
    }
}
