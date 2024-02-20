using Domain.Entities.Master;
using Domain.Repositories.Base;
using Domain.Repositories.Master;
using Domain.RequestFeatured;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;

namespace Persistence.Repositories.Master
{
    public class ProvinsiRepository : RepositoryBase<Provinsi>, IRepositoryWithPaging<Provinsi>
    {
        public ProvinsiRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(Provinsi entity)
        {
            Create(entity);
        }

        public void DeleteEntity(Provinsi entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<Provinsi>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(prov => prov.ProvId).ToListAsync();
        }

        public async Task<PagedList<Provinsi>> GetWithPaging(EntityParameter entityParams, bool trackChanges)
        {
            //with searchby
            var provinces = GetByCondition(p => p.ProvName.Contains(entityParams.SearchBy), false).OrderBy(p => p.ProvId);

            return PagedList<Provinsi>.ToPagedList(
                provinces,
                entityParams.PageNumber,
                entityParams.PageSize);
        }

        public async Task<Provinsi> GetEntityById(int id, bool trackChanges)
        {
            return await GetByCondition(prov => prov.ProvId.Equals(id), trackChanges).SingleOrDefaultAsync();
        }
    }
}