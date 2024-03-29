﻿using Domain.Entities.Partners;
using Domain.Entities.Users;
using Domain.Enum;
using Domain.Exceptions;
using Domain.Repositories.Partners;
using Domain.RequestFeatured;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;

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

        public async Task<IEnumerable<PartnerContact>> GetByUserId(int pacoUserEntityid, bool trackChanges)
        {
            return await GetByCondition(c => c.PacoUserEntityid.Equals(pacoUserEntityid), false).ToListAsync();
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
                          UserRoles = c.PacoUserEntity.UserRoles
                         .Where(x => x.UsroRoleName.Equals(EnumRoleType.PR))
                         .Select(x => new UserRole
                         {
                             UsroRoleName = x.UsroRoleName,
                             UsroStatus = x.UsroStatus
                         }).ToList()
                      },
                      PacoPatrnEntity = new Partner
                      {
                          PartEntityid = c.PacoPatrnEntity.PartEntityid,
                          PartName = c.PacoPatrnEntity.PartName,
                      }
                  }).ToListAsync();
        }

        public async Task<PagedList<PartnerContact>> GetAllPagingAsync(bool trackChanges, EntityParameter parameter)
        {
            IQueryable<PartnerContact> partnerContacts = _dbContext.PartnerContacts.AsNoTracking()
                .Where(x =>
                    x.PacoPatrnEntity != null && x.PacoPatrnEntity.PartName != null &&
                    EF.Functions.Like(x.PacoPatrnEntity.PartName, $"%{parameter.SearchBy}%") ||
                    x.PacoUserEntity != null && x.PacoUserEntity.UserFullName != null &&
                    EF.Functions.Like(x.PacoUserEntity.UserFullName, $"%{parameter.SearchBy}%")
                )
                .OrderBy(x => x.PacoUserEntityid)
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
                        UserRoles = c.PacoUserEntity.UserRoles
                        .Where(x => x.UsroRoleName.Equals(EnumRoleType.PR))
                        .Select(x => new UserRole
                        {
                            UsroRoleName = x.UsroRoleName,
                            UsroStatus = x.UsroStatus
                        }).ToList()
                    },
                    PacoPatrnEntity = new Partner
                    {
                        PartEntityid = c.PacoPatrnEntity.PartEntityid,
                        PartName = c.PacoPatrnEntity.PartName,
                    }
                });
            return PagedList<PartnerContact>.ToPagedList(partnerContacts, parameter.PageNumber, parameter.PageSize);
        }

        public async Task<PartnerContact> GetEntityById(int pacoPatrnEntityid, int pacoUserEntityid, bool trackChanges)
        {
            PartnerContact partnerContact = await GetByCondition(c => c.PacoPatrnEntityid.Equals(pacoPatrnEntityid) && c.PacoUserEntityid.Equals(pacoUserEntityid), trackChanges)
                .SingleOrDefaultAsync() ?? throw new EntityNotFoundException(pacoPatrnEntityid, nameof(PartnerContact));
            return partnerContact;
        }

        public Task<PartnerContact?> GetEntityById(int id, bool trackChanges)
        {
            throw new NotImplementedException();
        }
    }
}
