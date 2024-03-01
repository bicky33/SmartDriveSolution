using Domain.Entities.SO;
using Domain.Repositories.Partners;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Persistence.Repositories.Partners
{
    public class RepositoryPartnerClaimAssetSparepartBatch : IRepositoryPartnerClaimAssetSparepartBatch
    {
        private readonly SmartDriveContext _context;

        public RepositoryPartnerClaimAssetSparepartBatch(SmartDriveContext context)
        {
            _context = context;
        }

        public async Task CreateBatch(IEnumerable<ClaimAssetSparepart> data)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                await _context.ClaimAssetSpareparts.AddRangeAsync(data);
                await _context.SaveChangesAsync();
                transaction.Commit();

            } catch (Exception)
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
                List<ClaimAssetSparepart> data = await _context.ClaimAssetSpareparts
                    .Where(x => x.CaspPartEntityid.Equals(CaspPartEntityid) 
                    && x.CaspSeroId != null 
                    && x.CaspSeroId.Equals(CaspSeroId))
                    .ToListAsync();
                _context.ClaimAssetSpareparts.RemoveRange(data);
                await _context.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

    }
}
