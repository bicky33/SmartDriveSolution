using Contract.DTO.Partners;
using Domain.Repositories.Partners;
using Domain.RequestFeatured;
using Mapster;
using Service.Abstraction.Partners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Partners
{
    public class ServicePartnerContact : IServicePartnerContact
    {
        private readonly IRepositoryPartnerManager _repositoryPartnerMannager;

        public ServicePartnerContact(IRepositoryPartnerManager repositoryPartnerMannager)
        {
            _repositoryPartnerMannager = repositoryPartnerMannager;
        }

        public Task<PartnerContactDTO> CreateAsync(PartnerDTO entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int pacoPatrnEntityid, int pacoUserEntityid)
        {
            var partnerContact = await _repositoryPartnerMannager.RepositoryPartnerContact.GetEntityById(pacoPatrnEntityid, pacoUserEntityid, true);
            _repositoryPartnerMannager.RepositoryPartnerContact.DeleteEntity(partnerContact);
            await _repositoryPartnerMannager.UnitOfWorks.SaveChangesAsync();
        }

        public async Task<IEnumerable<PartnerContactResponse>> GetAllAsync(bool trackChanges)
        {
            var  partnerContacts =  await _repositoryPartnerMannager.RepositoryPartnerContact.GetAllEntity(trackChanges);
            var partnerContactDTO = partnerContacts.Adapt<IEnumerable<PartnerContactResponse>>();
            return partnerContactDTO;
        }

        public async Task<IEnumerable<PartnerContactResponse>> GetAllPagingAsync(EntityParameter parameter)
        {
            var partnerContact = await _repositoryPartnerMannager.RepositoryPartnerContact.GetAllPagingAsync(false, parameter);
            var partnerContactDTO = partnerContact.Adapt<IEnumerable<PartnerContactResponse>>();
            return partnerContactDTO;

        }
        public async Task<PartnerContactDTO> GetByIdAsync(int pacoPatrnEntityid, int pacoUserEntityid, bool trackChanges)
        {
            var partnerContact = await _repositoryPartnerMannager.RepositoryPartnerContact.GetEntityById(pacoPatrnEntityid, pacoUserEntityid, trackChanges);
            var partnerContactDTO = partnerContact.Adapt<PartnerContactDTO>();
            return partnerContactDTO;
        }

        public Task UpdateAsync(int id, PartnerDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
