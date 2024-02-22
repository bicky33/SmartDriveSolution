using Domain.Entities.Master;
using Domain.Repositories.Base;
using Domain.Repositories.Master;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;

namespace Persistence.Repositories.Master
{
    public class TemplateInsurancePremiRespository : RepositoryBase<TemplateInsurancePremi>, IRepositoryTemplateInsurancePremi
    {
        public TemplateInsurancePremiRespository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(TemplateInsurancePremi entity)
        {
            Create(entity);
        }

        public void DeleteEntity(TemplateInsurancePremi entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<TemplateInsurancePremi>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(temi => temi.TemiId).ToListAsync();
        }

        public async Task<TemplateInsurancePremi> GetEntityById(int id, bool trackChanges)
        {
            return await GetByCondition(temi => temi.TemiId.Equals(id), trackChanges).SingleOrDefaultAsync();
        }

        public async Task<TemplateInsurancePremi> GetTemiByCateIDIntyNameZoneID(int cateId, string intyName, int zoneId, bool trackChanges)
        {
            return await GetByCondition(temi => temi.TemiCateId.Equals(cateId) &&  temi.TemiIntyName.Equals(intyName) && temi.TemiZonesId.Equals(zoneId), trackChanges).SingleOrDefaultAsync();
            
        }
    }
}