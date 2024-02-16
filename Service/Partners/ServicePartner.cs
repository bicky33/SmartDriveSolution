using Contract.DTO.Partners;
using Domain.Entities.Partners;
using Domain.Entities.Users;
using Domain.Enum;
using Domain.Exceptions;
using Domain.Repositories.Partners;
using Domain.RequestFeatured;
using Mapster;
using Service.Abstraction.Base;
using Service.Abstraction.Partners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Partners
{
    public class ServicePartner : IServicePartner
    {
        private readonly IRepositoryPartnerManager _partnerManagerRepository;

        public ServicePartner(IRepositoryPartnerManager partnerManagerRepository)
        {
            _partnerManagerRepository = partnerManagerRepository;
        }

        public async Task<PartnerDTO> CreateAsync(PartnerDTO entity)
        {
            var business = _partnerManagerRepository.RepositoryBusinessEntity.CreateEntity();
            await _partnerManagerRepository.UnitOfWorks.SaveChangesAsync();
            var partner = entity.Adapt<Partner>();
            partner.PartEntityid = business.Entityid;
            _partnerManagerRepository.RepositoryPartner.CreateEntity(partner);
            await _partnerManagerRepository.UnitOfWorks.SaveChangesAsync();
            var response = entity with
            {
                PartEntityid = business.Entityid
            };
            return response;
        }

        public async Task DeleteAsync(int id)
        {
            var partner = await _partnerManagerRepository.RepositoryPartner.GetEntityById(id, true);
             _partnerManagerRepository.RepositoryPartner.DeleteEntity(partner);
            await _partnerManagerRepository.UnitOfWorks.SaveChangesAsync();
        }

        public async Task<IEnumerable<PartnerDTO>> GetAllAsync(bool trackChanges)
        {
            IEnumerable<Partner> partners = await _partnerManagerRepository.RepositoryPartner.GetAllEntity(false);
            IEnumerable<PartnerDTO> partnersDto = partners.Adapt<IEnumerable<PartnerDTO>>();
            return partnersDto;
        }

        public async Task<IEnumerable<PartnerDTO>> GetAllPagingAsync(EntityParameter parameter, bool trackChanges)
        {
            var partners = await _partnerManagerRepository.RepositoryPartner.GetAllPaging(trackChanges, parameter);
            var partnersDTO = partners.Adapt<IEnumerable<PartnerDTO>>();
            return partnersDTO;
        }

        public async Task<PartnerDTO> GetByIdAsync(int id, bool trackChanges)
        {
            var partner = await _partnerManagerRepository.RepositoryPartner.GetEntityById(id, true);
            var partnerDto = partner.Adapt<PartnerDTO>();
            return partnerDto;
        }

        public async Task UpdateAsync(int id, PartnerDTO entity)
        {
            var partner = await _partnerManagerRepository.RepositoryPartner.GetEntityById(id, true);
            partner.PartAccountNo = entity.PartAccountNo;
            partner.PartAddress = entity.PartAddress;
            partner.PartNpwp = entity.PartNpwp;
            partner.PartName = entity.PartName;
            partner.PartCityId = entity.PartCityId;
            partner.PartStatus = entity.PartStatus.ToString();
            partner.PartModifiedDate = DateTime.Now;
            await _partnerManagerRepository.UnitOfWorks.SaveChangesAsync();
        }
    }
}
