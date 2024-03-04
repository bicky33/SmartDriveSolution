using Domain.Entities.SO;
using Domain.Repositories.Partners;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories.Partners
{
    public class RepositoryPartnerClaimAssetEvidenceBatch : IRepositoryPartnerClaimAssetEvidenceBatch
    {
        private readonly SmartDriveContext _context;

        public RepositoryPartnerClaimAssetEvidenceBatch(SmartDriveContext context)
        {
            _context = context;
        }

        public async Task CreateBatch(IEnumerable<ClaimAssetEvidence> data)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                await _context.ClaimAssetEvidences.AddRangeAsync(data);
                await _context.SaveChangesAsync();
                transaction.Commit();

            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task DeleteBatch(int CaspPartEntityid, string CaspSeroId)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                List<ClaimAssetEvidence> data = await _context.ClaimAssetEvidences
                    .Where(x => x.CaevPartEntityid.Equals(CaspPartEntityid)
                    && x.CaevSeroId != null
                    && x.CaevSeroId.Equals(CaspSeroId))
                    .ToListAsync();
                _context.ClaimAssetEvidences.RemoveRange(data);
                await _context.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<IEnumerable<ClaimAssetEvidence>> GetData(int CaspPartEntityid, string CaspSeroId)
        {
            return await _context.ClaimAssetEvidences.AsNoTracking()
                   .Where(x => x.CaevPartEntityid.Equals(CaspPartEntityid)
                   && x.CaevSeroId != null
                   && x.CaevSeroId.Equals(CaspSeroId)).ToListAsync();
        }
    }
}
