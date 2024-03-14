using Domain.Entities.HR;
using Domain.Entities.Master;
using Domain.Entities.Partners;
using Domain.Repositories.HR;
using Domain.RequestFeatured;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.HR
{
    public class EmployeeArwgRepository : RepositoryBase<EmployeeAreWorkgroup>, IEmployeeArwgRepository
    {
        public EmployeeArwgRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(EmployeeAreWorkgroup entity)
        {
            Create(entity);
        }

        public void DeleteEntity(EmployeeAreWorkgroup entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<EmployeeAreWorkgroup>> GetAllEntity(bool trackChanges)
        {
            IQueryable<EmployeeAreWorkgroup> result = _dbContext.EmployeeAreWorkgroups.AsNoTracking().Where(x => x.SoftDelete == "ACTIVE")
                 .Include(c => c.EawgArwgCodeNavigation)
                     .ThenInclude(d => d.ArwgCity)
                         .ThenInclude(e => e.CityProv)
                             .ThenInclude(f => f.ProvZones)
                 .Include(g => g.EawgEntity).Where(x => x.EawgEntity.SoftDelete == "ACTIVE")
             .Select(c => new EmployeeAreWorkgroup
             {
                 EawgId = c.EawgId,
                 EawgArwgCodeNavigation = new AreaWorkgroup
                 {
                     ArwgCode = c.EawgArwgCodeNavigation.ArwgCode,
                     ArwgCity = new City
                     {
                         CityId = c.EawgArwgCodeNavigation.ArwgCity.CityId,
                         CityName = c.EawgArwgCodeNavigation.ArwgCity.CityName,
                         CityProv = new Provinsi
                         {
                             ProvId = c.EawgArwgCodeNavigation.ArwgCity.CityProv.ProvId,
                             ProvName = c.EawgArwgCodeNavigation.ArwgCity.CityProv.ProvName,
                             ProvZones = new Zone
                             {
                                 ZonesId = c.EawgArwgCodeNavigation.ArwgCity.CityProv.ProvZones.ZonesId,
                                 ZonesName = c.EawgArwgCodeNavigation.ArwgCity.CityProv.ProvZones.ZonesName,
                             }
                         }
                     }
                 },
                 EawgEntity = new Employee
                 {
                     EmpName = c.EawgEntity.EmpName,
                 }
             });


            return result;
        }

        public async Task<EmployeeAreWorkgroup> GetEntityById(int id, bool trackChanges)
        {
            return await GetByCondition(c => c.EawgId.Equals(id), trackChanges).SingleOrDefaultAsync();
        }

        public async Task<PagedList<EmployeeAreWorkgroup>> GetAllPaging(EntityParameter entityParams, bool trackChanges)
        {
            IQueryable<EmployeeAreWorkgroup> arwg = _dbContext.EmployeeAreWorkgroups.AsNoTracking().Where(x => x.SoftDelete == "ACTIVE")
                 .Include(c => c.EawgArwgCodeNavigation)
                     .ThenInclude(d => d.ArwgCity)
                         .ThenInclude(e => e.CityProv)
                             .ThenInclude(f => f.ProvZones)
                 .Include(g => g.EawgEntity).Where(x => x.EawgEntity.SoftDelete == "ACTIVE" && EF.Functions.Like(x.EawgEntity.EmpName, $"%{entityParams.SearchBy}%"))
             .Select(c => new EmployeeAreWorkgroup
             {
                 EawgId = c.EawgId,
                 EawgArwgCodeNavigation = new AreaWorkgroup
                 {
                     ArwgCode = c.EawgArwgCodeNavigation.ArwgCode,
                     ArwgCity = new City
                     {
                         CityId = c.EawgArwgCodeNavigation.ArwgCity.CityId,
                         CityName = c.EawgArwgCodeNavigation.ArwgCity.CityName,
                         CityProv = new Provinsi
                         {
                             ProvId = c.EawgArwgCodeNavigation.ArwgCity.CityProv.ProvId,
                             ProvName = c.EawgArwgCodeNavigation.ArwgCity.CityProv.ProvName,
                             ProvZones = new Zone
                             {
                                 ZonesId = c.EawgArwgCodeNavigation.ArwgCity.CityProv.ProvZones.ZonesId,
                                 ZonesName = c.EawgArwgCodeNavigation.ArwgCity.CityProv.ProvZones.ZonesName,
                             }
                         }
                     }
                 },
                 EawgEntity = new Employee
                 {
                     EmpName = c.EawgEntity.EmpName,
                 }
             });
            return  PagedList<EmployeeAreWorkgroup>.ToPagedList(arwg, entityParams.PageNumber, entityParams.PageSize);
        }

        public async Task<IEnumerable<EmployeeAreWorkgroup>> FindEmployeeById(int id)
        {
            IQueryable<EmployeeAreWorkgroup> result = _dbContext.EmployeeAreWorkgroups.AsNoTracking().Where(x => x.SoftDelete == "ACTIVE" && x.EawgId == id)
                 .Include(c => c.EawgArwgCodeNavigation)
                     .ThenInclude(d => d.ArwgCity)
                         .ThenInclude(e => e.CityProv)
                             .ThenInclude(f => f.ProvZones)
                 .Include(g => g.EawgEntity).Where(x => x.EawgEntity.SoftDelete == "ACTIVE")
             .Select(c => new EmployeeAreWorkgroup
             {
                 EawgId = c.EawgId,
                 EawgArwgCodeNavigation = new AreaWorkgroup
                 {
                     ArwgCode = c.EawgArwgCodeNavigation.ArwgCode,
                     ArwgCity = new City
                     {
                         CityId = c.EawgArwgCodeNavigation.ArwgCity.CityId,
                         CityName = c.EawgArwgCodeNavigation.ArwgCity.CityName,
                         CityProv = new Provinsi
                         {
                             ProvId = c.EawgArwgCodeNavigation.ArwgCity.CityProv.ProvId,
                             ProvName = c.EawgArwgCodeNavigation.ArwgCity.CityProv.ProvName,
                             ProvZones = new Zone
                             {
                                 ZonesId = c.EawgArwgCodeNavigation.ArwgCity.CityProv.ProvZones.ZonesId,
                                 ZonesName = c.EawgArwgCodeNavigation.ArwgCity.CityProv.ProvZones.ZonesName,
                             }
                         }
                     }
                 },
                 EawgEntity = new Employee
                 {
                     EmpName = c.EawgEntity.EmpName,
                 }
             });



            return result;
        }

        public async Task<EmployeeAreWorkgroup> FindArwgById(int id)
        {
          return await _dbContext.EmployeeAreWorkgroups.AsNoTracking().Where(c => c.SoftDelete == "ACTIVE" && c.EawgId == id)
                 .Include(c => c.EawgArwgCodeNavigation)
             .Select(c => new EmployeeAreWorkgroup
             {
                 EawgArwgCode = c.EawgArwgCode,
                 EawgArwgCodeNavigation = new AreaWorkgroup
                 {
                     ArwgCityId = c.EawgArwgCodeNavigation.ArwgCityId
                 },
             }).FirstOrDefaultAsync();

        }
    }
}
