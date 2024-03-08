using Contract.DTO.Partners;
using Contract.Records;
using Domain.Entities.Partners;
using Domain.Repositories.Partners;
using Domain.RequestFeatured;
using Mapster;
using Service.Abstraction.Partners;
using System.Transactions;

namespace Service.Partners
{
    public class ServicePartnerAreaWorkgroup : IServicePartnerAreaWorkgroup
    {
        private readonly IRepositoryPartnerManager _repositoryPartnerManager;

        public ServicePartnerAreaWorkgroup(IRepositoryPartnerManager repositoryPartnerManager)
        {
            _repositoryPartnerManager = repositoryPartnerManager;
        }

        public async Task CreateAsync(PartnerAreaWorkgroupDTO entity)
        {
            PartnerAreaWorkgroup partnerAreaWorkgroups = entity.Adapt<PartnerAreaWorkgroup>();
            _repositoryPartnerManager.RepositoryPartnerAreaWorkgroup.CreateEntity(partnerAreaWorkgroups);
            await _repositoryPartnerManager.UnitOfWorks.SaveChangesAsync();
        }
        public async Task DeleteAsync(int PawoPatrEntityid, string PawoArwgCode, int PawoUserEntityid)
        {
            PartnerAreaWorkgroup partnerAreaWorkgroup = await _repositoryPartnerManager.RepositoryPartnerAreaWorkgroup.GetEntityById(true, PawoPatrEntityid, PawoUserEntityid, PawoArwgCode);
            _repositoryPartnerManager.RepositoryPartnerAreaWorkgroup.DeleteEntity(partnerAreaWorkgroup);
            await _repositoryPartnerManager.UnitOfWorks.SaveChangesAsync();
        }

        public async Task<IEnumerable<PartnerAreaWorkgroupResponse>> GetAllAsync(bool trackChanges)
        {
            IEnumerable<PartnerAreaWorkgroup> partnerAreaWorkgroups = await _repositoryPartnerManager.RepositoryPartnerAreaWorkgroup.GetAllEntity(trackChanges);
            IEnumerable<PartnerAreaWorkgroupResponse> partnerAreaWorkgroupsDTO = partnerAreaWorkgroups.Adapt<IEnumerable<PartnerAreaWorkgroupResponse>>();
            return partnerAreaWorkgroupsDTO;
        }

        public async Task<PaginationDTO<PartnerAreaWorkgroupResponse>> GetAllPagingAsync(EntityParameter parameter, bool trackChanges)
        {
            PagedList<PartnerAreaWorkgroup> partnerAreaWorkgroups = await _repositoryPartnerManager.RepositoryPartnerAreaWorkgroup.GetAllPaging(trackChanges, parameter);
            IEnumerable<PartnerAreaWorkgroupResponse> partnerAreaWorkgroupsDTO = partnerAreaWorkgroups.Adapt<IEnumerable<PartnerAreaWorkgroupResponse>>();
            PaginationDTO<PartnerAreaWorkgroupResponse> response = new(partnerAreaWorkgroups.TotalPages, partnerAreaWorkgroups.CurrentPage, partnerAreaWorkgroupsDTO.ToList());
            return response;
        }

        public async Task<PartnerAreaWorkgroupDTO> GetByIdAsync(
            int PawoPatrEntityid,
            string PawoArwgCode,
            int PawoUserEntityid,
            bool trackChanges
        )
        {
            PartnerAreaWorkgroup partnerAreaWorkgroup = await _repositoryPartnerManager.RepositoryPartnerAreaWorkgroup.GetEntityById(trackChanges, PawoPatrEntityid, PawoUserEntityid, PawoArwgCode);
            PartnerAreaWorkgroupDTO partnerAreaWorkgroupDTO = partnerAreaWorkgroup.Adapt<PartnerAreaWorkgroupDTO>();
            return partnerAreaWorkgroupDTO;
        }

        public async Task UpdateAsync(
            int PawoPatrEntityid,
            string PawoArwgCode,
            int PawoUserEntityid, 
            PartnerAreaWorkgroupDTO entity,
            bool trackChanges
        )
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                PartnerAreaWorkgroup partnerAreaWorkgroup = await _repositoryPartnerManager.RepositoryPartnerAreaWorkgroup.GetEntityById(trackChanges, PawoPatrEntityid, PawoUserEntityid, PawoArwgCode);
                _repositoryPartnerManager.RepositoryPartnerAreaWorkgroup.DeleteEntity(partnerAreaWorkgroup);
                await _repositoryPartnerManager.UnitOfWorks.SaveChangesAsync();
                PartnerAreaWorkgroup data = entity.Adapt<PartnerAreaWorkgroup>();
                _repositoryPartnerManager.RepositoryPartnerAreaWorkgroup.CreateEntity(data);
                await _repositoryPartnerManager.UnitOfWorks.SaveChangesAsync();
                scope.Complete();
            }
            catch( Exception )
            {
                throw;
            }
        }

        public async Task<IEnumerable<PartnerAreaWorkgroupDTO>> GetByPartnerAndUserId(int pawoUserId, int pawoPatrId, bool trackChanges)
        {
            IEnumerable<PartnerAreaWorkgroup> areas = await _repositoryPartnerManager.RepositoryPartnerAreaWorkgroup.GetByPartnerAndUserId(pawoUserId, pawoPatrId, trackChanges);
            IEnumerable<PartnerAreaWorkgroupDTO> areasDTO = areas.Adapt<IEnumerable<PartnerAreaWorkgroupDTO>>();
            return areasDTO;
        }
    }
}
