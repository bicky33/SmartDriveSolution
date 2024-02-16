using Contract.DTO.Partners;
using Domain.Entities.Partners;
using Domain.Repositories.Partners;
using Domain.RequestFeatured;
using Mapster;
using Service.Abstraction.Partners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var partnerAreaWorkgroups = entity.Adapt<PartnerAreaWorkgroup>();
            _repositoryPartnerManager.RepositoryPartnerAreaWorkgroup.CreateEntity(partnerAreaWorkgroups);
            await _repositoryPartnerManager.UnitOfWorks.SaveChangesAsync();
        }
        public async Task DeleteAsync(int PawoPatrEntityid, string PawoArwgCode, int PawoUserEntityid)
        {
            var partnerAreaWorkgroup = await _repositoryPartnerManager.RepositoryPartnerAreaWorkgroup.GetEntityById(true, PawoPatrEntityid, PawoUserEntityid, PawoArwgCode);
            _repositoryPartnerManager.RepositoryPartnerAreaWorkgroup.DeleteEntity(partnerAreaWorkgroup);
            await _repositoryPartnerManager.UnitOfWorks.SaveChangesAsync();
        }

        public async Task<IEnumerable<PartnerAreaWorkgroupResponse>> GetAllAsync(bool trackChanges)
        {
            var partnerAreaWorkgroups = await _repositoryPartnerManager.RepositoryPartnerAreaWorkgroup.GetAllEntity(trackChanges);
            var partnerAreaWorkgroupsDTO = partnerAreaWorkgroups.Adapt<IEnumerable<PartnerAreaWorkgroupResponse>>();
            return partnerAreaWorkgroupsDTO;
        }

        public async Task<IEnumerable<PartnerAreaWorkgroupResponse>> GetAllPagingAsync(EntityParameter parameter, bool trackChanges)
        {
            var partnerAreaWorkgroups = await _repositoryPartnerManager.RepositoryPartnerAreaWorkgroup.GetAllPaging(trackChanges, parameter);
            var partnerAreaWorkgroupsDTO = partnerAreaWorkgroups.Adapt<IEnumerable<PartnerAreaWorkgroupResponse>>();
            return partnerAreaWorkgroupsDTO;
        }

        public async Task<PartnerAreaWorkgroupDTO> GetByIdAsync(
            int PawoPatrEntityid,
            string PawoArwgCode,
            int PawoUserEntityid,
            bool trackChanges
        )
        {
            var partnerAreaWorkgroup = await _repositoryPartnerManager.RepositoryPartnerAreaWorkgroup.GetEntityById(trackChanges, PawoPatrEntityid, PawoUserEntityid, PawoArwgCode);
            var partnerAreaWorkgroupDTO = partnerAreaWorkgroup.Adapt<PartnerAreaWorkgroupDTO>();
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
                var partnerAreaWorkgroup = await _repositoryPartnerManager.RepositoryPartnerAreaWorkgroup.GetEntityById(trackChanges, PawoPatrEntityid, PawoUserEntityid, PawoArwgCode);
                _repositoryPartnerManager.RepositoryPartnerAreaWorkgroup.DeleteEntity(partnerAreaWorkgroup);
                await _repositoryPartnerManager.UnitOfWorks.SaveChangesAsync();
                var data = entity.Adapt<PartnerAreaWorkgroup>();
                _repositoryPartnerManager.RepositoryPartnerAreaWorkgroup.CreateEntity(data);
                await _repositoryPartnerManager.UnitOfWorks.SaveChangesAsync();
                scope.Complete();
            }
            catch( Exception )
            {
                throw;
            }
        }
    }
}
