using Domain.Entities.Partners;
using Domain.Entities.Users;
using Domain.Exceptions;
using Domain.Repositories.Base;
using Domain.Repositories.Partners;
using Domain.RequestFeatured;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Partners
{
    public class RepositoryPartnerContact : RepositoryBase<PartnerContact>, IRepositoryPartnerContact
    {
        public RepositoryPartnerContact(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(PartnerContact entity)
        {
            Create(entity);
        }

        public void DeleteEntity(PartnerContact entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<PartnerContact>> GetAllEntity(bool trackChanges)
        {
           return await _dbContext.PartnerContacts
                .Include(c => c.PacoUserEntity)
                .Select(c => new PartnerContact
                {
                    PacoPatrnEntityid = c.PacoPatrnEntityid,
                    PacoUserEntityid = c.PacoUserEntityid,
                    PacoStatus = c.PacoStatus,
                    PacoUserEntity = new User
                    {
                        UserFullName = c.PacoUserEntity.UserFullName,
                        UserPhones = c.PacoUserEntity.UserPhones.Select(d => new UserPhone
                        {
                            UsphEntityid = d.UsphEntityid,
                            UsphPhoneNumber = d.UsphPhoneNumber,
                            UsphPhoneType = d.UsphPhoneType,
                            UsphStatus = d.UsphStatus
                        }).ToList(),
                    }
                }).ToListAsync();
        }

        public async Task<PagedList<PartnerContact>> GetAllPagingAsync(bool trackChanges, EntityParameter parameter)
        {
            var partnerContacts = _dbContext.PartnerContacts
                .Include(c => c.PacoUserEntity)
                .Select(c => new PartnerContact
                {
                    PacoPatrnEntityid = c.PacoPatrnEntityid,
                    PacoUserEntityid = c.PacoUserEntityid,
                    PacoStatus = c.PacoStatus,
                    PacoUserEntity = new User
                    {
                        UserFullName = c.PacoUserEntity.UserFullName,
                        UserPhones = c.PacoUserEntity.UserPhones.Select(d => new UserPhone
                        {
                            UsphEntityid = d.UsphEntityid,
                            UsphPhoneNumber = d.UsphPhoneNumber,
                            UsphPhoneType = d.UsphPhoneType,
                            UsphStatus = d.UsphStatus
                        }).ToList(),
                    }
                });
            return PagedList<PartnerContact>.ToPagedList(partnerContacts, parameter.PageNumber, parameter.PageSize);
        }

        public async Task<PartnerContact> GetEntityById(int pacoPatrnEntityid, int pacoUserEntityid, bool trackChanges)
        {
            var partnerContact =  await GetByCondition(c => c.PacoPatrnEntity.Equals(pacoPatrnEntityid) && c.PacoUserEntityid.Equals(pacoUserEntityid), trackChanges)
                .FirstOrDefaultAsync() ?? throw new EntityNotFoundException(pacoPatrnEntityid, nameof(PartnerContact));
            return partnerContact;
        }

        public Task<PartnerContact?> GetEntityById(int id, bool trackChanges)
        {
            throw new NotImplementedException();
        }
    }
}
