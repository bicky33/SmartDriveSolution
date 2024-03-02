using Domain.Entities.Master;
using Domain.Entities.Partners;
using Domain.Exceptions;
using Domain.Repositories.Base;
using Domain.Repositories.Partners;
using Domain.RequestFeatured;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;


namespace Persistence.Repositories.Partners
{
    public class RepositoryPartner : RepositoryBase<Partner>, IRepositoryPartner
    {
        public RepositoryPartner(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(Partner entity)
        {
            Create(entity);
        }

        public void DeleteEntity(Partner entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<Partner>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(c => c.PartEntityid).ToListAsync();
        }

        public async Task<Partner> GetEntityById(int id, bool trackChanges)
        {
            Partner partner = await GetByCondition(c => c.PartEntityid.Equals(id), trackChanges).SingleOrDefaultAsync() 
                ?? throw new EntityNotFoundException(id, nameof(Partner));
            return partner;
        }

        public async Task<PagedList<Partner>> GetAllPaging(bool trackChanges, EntityParameter parameter)
        {
            IQueryable<Partner> partners;

            if (string.IsNullOrEmpty(parameter.SearchBy))
            {
                partners = partners = _dbContext.Partners
                    .Select(xx => new Partner()
                    {
                        PartEntityid = xx.PartEntityid,
                        PartNpwp = xx.PartNpwp,
                        PartAccountNo = xx.PartAccountNo,
                        PartName = xx.PartName,
                        PartAddress = xx.PartAddress,
                        PartJoinDate = xx.PartJoinDate,
                        PartStatus = xx.PartStatus,
                        PartCityId = xx.PartCityId,
                        PartCity = new City
                        {
                            CityId = xx.PartCity.CityId,
                            CityName = xx.PartCity.CityName
                        }
                    });
            } else
            {
                partners = _dbContext.Partners
                    .Where(x =>
                    x.PartName != null &&
                    x.PartName.Contains(parameter.SearchBy) ||
                    x.PartNpwp != null && x.PartNpwp.Contains(parameter.SearchBy)
                    ).Select(xx => new Partner()
                    {
                        PartEntityid = xx.PartEntityid,
                        PartNpwp = xx.PartNpwp,
                        PartAccountNo = xx.PartAccountNo,
                        PartName = xx.PartName,
                        PartAddress = xx.PartAddress,
                        PartJoinDate = xx.PartJoinDate,
                        PartStatus = xx.PartStatus,
                        PartCityId = xx.PartCityId,
                        PartCity = new City
                        {
                            CityId = xx.PartCity.CityId,
                            CityName = xx.PartCity.CityName
                        }
                    });
            }
 
            return PagedList<Partner>.ToPagedList(partners, parameter.PageNumber, parameter.PageSize);

        }
    }
}
