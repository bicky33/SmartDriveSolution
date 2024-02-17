using Domain.Entities.Master;
using Domain.Entities.Partners;
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
    internal class RepositoryPartnerAreaWorkgroup : RepositoryBase<PartnerAreaWorkgroup>, IRepositoryPartnerAreaWorkgroup
    {
        public RepositoryPartnerAreaWorkgroup(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(PartnerAreaWorkgroup entity)
        {
            Create(entity);
        }

        public void DeleteEntity(PartnerAreaWorkgroup entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<PartnerAreaWorkgroup>> GetAllEntity(bool trackChanges)
        {
            IQueryable<PartnerAreaWorkgroup> result = _dbContext.PartnerAreaWorkgroups.AsNoTracking()
                .Include(c => c.PawoArwgCodeNavigation)
                    .ThenInclude(d => d.ArwgCity)
                        .ThenInclude(e => e.CityProv)
                            .ThenInclude(f => f.ProvZones)
                .Include(g => g.Pawo)
                    .ThenInclude(h => h.PacoPatrnEntity)
            .Select(c => new PartnerAreaWorkgroup
            {
                PawoArwgCode = c.PawoArwgCode,
                PawoPatrEntityid = c.PawoPatrEntityid,
                PawoUserEntityid = c.PawoUserEntityid,
                PawoArwgCodeNavigation = new AreaWorkgroup
                {
                    ArwgCode = c.PawoArwgCodeNavigation.ArwgCode,
                    ArwgDesc = c.PawoArwgCodeNavigation.ArwgDesc,
                    ArwgCity = new City
                    {
                        CityId = c.PawoArwgCodeNavigation.ArwgCity.CityId,
                        CityName = c.PawoArwgCodeNavigation.ArwgCity.CityName,
                        CityProv = new Provinsi
                        {
                            ProvId = c.PawoArwgCodeNavigation.ArwgCity.CityProv.ProvId,
                            ProvName = c.PawoArwgCodeNavigation.ArwgCity.CityProv.ProvName,
                            ProvZones = new Zone
                            {
                                ZonesId = c.PawoArwgCodeNavigation.ArwgCity.CityProv.ProvZones.ZonesId,
                                ZonesName = c.PawoArwgCodeNavigation.ArwgCity.CityProv.ProvZones.ZonesName,
                            }
                        }
                    }
                },
                Pawo = new PartnerContact
                {
                    PacoPatrnEntityid = c.Pawo.PacoPatrnEntityid,
                    PacoPatrnEntity = new Partner
                    {
                        PartEntityid = c.Pawo.PacoPatrnEntity.PartEntityid,
                        PartName = c.Pawo.PacoPatrnEntity.PartName,
                    }
                }
            });

            return result;
        }

        public async Task<PagedList<PartnerAreaWorkgroup>> GetAllPaging(bool trackChanges, EntityParameter parameter)
        {
            IQueryable<PartnerAreaWorkgroup> result = _dbContext.PartnerAreaWorkgroups
                .Include(c => c.PawoArwgCodeNavigation)
                    .ThenInclude(d => d.ArwgCity)
                        .ThenInclude(e => e.CityProv)
                            .ThenInclude(f => f.ProvZones)
                .Include(g => g.Pawo)
                    .ThenInclude(h => h.PacoPatrnEntity)
                .Select(c => new PartnerAreaWorkgroup
                {
                    PawoArwgCode = c.PawoArwgCode,
                    PawoPatrEntityid = c.PawoPatrEntityid,
                    PawoUserEntityid = c.PawoUserEntityid,
                    PawoArwgCodeNavigation = new AreaWorkgroup
                    {
                        ArwgCode = c.PawoArwgCodeNavigation.ArwgCode,
                        ArwgCity = new City
                        {
                            CityId = c.PawoArwgCodeNavigation.ArwgCity.CityId,
                            CityName = c.PawoArwgCodeNavigation.ArwgCity.CityName,
                            CityProv = new Provinsi
                            {
                                ProvId = c.PawoArwgCodeNavigation.ArwgCity.CityProv.ProvId,
                                ProvName = c.PawoArwgCodeNavigation.ArwgCity.CityProv.ProvName,
                                ProvZones = new Zone
                                {
                                    ZonesId = c.PawoArwgCodeNavigation.ArwgCity.CityProv.ProvZones.ZonesId,
                                    ZonesName = c.PawoArwgCodeNavigation.ArwgCity.CityProv.ProvZones.ZonesName,
                                }
                            }
                        }
                    },
                    Pawo = new PartnerContact
                    {
                        PacoPatrnEntityid = c.Pawo.PacoPatrnEntityid,
                        PacoPatrnEntity = new Partner
                        {
                            PartEntityid = c.Pawo.PacoPatrnEntity.PartEntityid,
                            PartName = c.Pawo.PacoPatrnEntity.PartName,
                        }
                    }
                });
            return PagedList<PartnerAreaWorkgroup>.ToPagedList(result, parameter.PageNumber, parameter.PageSize);
        }
        public async Task<PartnerAreaWorkgroup> GetEntityById(bool trackChanges, int partnerId, int userId, string areaWorkgroupCode)
        {
            PartnerAreaWorkgroup result = await GetByCondition(c =>
                c.PawoPatrEntityid.Equals(partnerId)
                && c.PawoUserEntityid.Equals(userId) &&
                c.PawoArwgCode.Equals(areaWorkgroupCode), trackChanges).FirstOrDefaultAsync() 
                ?? throw new EntityNotFoundException(partnerId, nameof(PartnerAreaWorkgroup));
            return result;
        }

        public Task<PartnerAreaWorkgroup> GetEntityById(int id, bool trackChanges)
        {
            throw new NotImplementedException();
        }
    }
}
