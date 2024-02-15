using Contract.DTO.SO;
using Domain.Exceptions.SO;
using Domain.Repositories.SO;
using Mapster;
using Service.Abstraction.SO;

namespace Service.SO
{
    public class ClaimAssetSparepartService : IServiceSOEntityBase<ClaimAssetSparepartDto,ClaimAssetSparepartDtoCreate, int>
    {
        private readonly IRepositorySOManager _repositoryManager;

        public ClaimAssetSparepartService(IRepositorySOManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

       public  async Task<ClaimAssetSparepartDtoCreate> CreateAsync(ClaimAssetSparepartDtoCreate entity)
        {
            var claimAssetSparepart = entity.Adapt<Domain.Entities.SO.ClaimAssetSparepart>();
            _repositoryManager.ClaimAssetSparepartRepository.CreateEntity(claimAssetSparepart);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();
            return claimAssetSparepart.Adapt<ClaimAssetSparepartDtoCreate>();
        }

        public async Task DeleteAsync(int id)
        {
            var claimAssetSparepart = await _repositoryManager.ClaimAssetSparepartRepository.GetEntityById(id,false);
            if (claimAssetSparepart == null)
                throw new EntityNotFoundException(id,"Claim Asset Sparepart");
            _repositoryManager.ClaimAssetSparepartRepository.DeleteEntity(claimAssetSparepart);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<ClaimAssetSparepartDto>> GetAllAsync(bool trackChanges)
        {
            var claimAssetSparepart = await _repositoryManager.ClaimAssetSparepartRepository.GetAllEntity(trackChanges);
            var ClaimAssetSparepartDtos = claimAssetSparepart.Adapt<IEnumerable<ClaimAssetSparepartDto>>();
            return ClaimAssetSparepartDtos;
        }

        public async Task<ClaimAssetSparepartDto> GetByIdAsync(int id, bool trackChanges)
        {
            var claimAssetSparepart = await _repositoryManager.ClaimAssetSparepartRepository.GetEntityById(id, trackChanges); 
            if (claimAssetSparepart == null)
                throw new EntityNotFoundException(id,"Claim Asset Sparepart");

            var ClaimAssetSparepartDtos = claimAssetSparepart.Adapt<ClaimAssetSparepartDto>();
            return ClaimAssetSparepartDtos;
        }

        public async Task<ClaimAssetSparepartDtoCreate> UpdateAsync(int id, ClaimAssetSparepartDtoCreate entity)
        {
            var claimAssetSparepart = await _repositoryManager.ClaimAssetSparepartRepository.GetEntityById(id, true);
            if (claimAssetSparepart == null)
                throw new EntityNotFoundException(id,"Claim Asset Sparepart");

            claimAssetSparepart.CaspId = id;
            claimAssetSparepart.CaspItemName = entity.CaspItemName;
            claimAssetSparepart.CaspQuantity = entity.CaspQuantity;
            claimAssetSparepart.CaspItemPrice = entity.CaspItemPrice;
            claimAssetSparepart.CaspSubtotal = entity.CaspSubtotal;
            claimAssetSparepart.CaspPartEntityid = entity.CaspPartEntityid;
            claimAssetSparepart.CaspSeroId = entity.CaspSeroId;
            claimAssetSparepart.CaspCreatedDate = entity.CaspCreatedDate;

            await _repositoryManager.UnitOfWork.SaveChangesAsync();
            return claimAssetSparepart.Adapt<ClaimAssetSparepartDtoCreate>();
        }
    }
}
