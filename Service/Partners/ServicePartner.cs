using Contract.DTO.Partners;
using Contract.Records;
using Domain.Entities.Master;
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

            BusinessEntity business = _partnerManagerRepository.RepositoryBusinessEntity.CreateEntity();
            await _partnerManagerRepository.UnitOfWorks.SaveChangesAsync();
            Partner partner = entity.Adapt<Partner>();
            partner.PartEntityid = business.Entityid;
            _partnerManagerRepository.RepositoryPartner.CreateEntity(partner);
            await _partnerManagerRepository.UnitOfWorks.SaveChangesAsync();
            City city = await _partnerManagerRepository.RepositoryCity.GetEntityById(entity.PartCityId, false);
            PartnerDTO response = entity with
            {
                PartEntityid = business.Entityid,
                CityName = city.CityName
            };
            return response;
        }

        public async Task DeleteAsync(int id)
        {
            Partner partner = await _partnerManagerRepository.RepositoryPartner.GetEntityById(id, true);
             _partnerManagerRepository.RepositoryPartner.DeleteEntity(partner);
            await _partnerManagerRepository.UnitOfWorks.SaveChangesAsync();
        }

        public async Task<IEnumerable<PartnerDTO>> GetAllAsync(bool trackChanges)
        {
            IEnumerable<Partner> partners = await _partnerManagerRepository.RepositoryPartner.GetAllEntity(false);
            IEnumerable<PartnerDTO> partnersDto = partners.Adapt<IEnumerable<PartnerDTO>>();
            return partnersDto;
        }

        public async Task<PaginationDTO<PartnerDTO>> GetAllPagingAsync(EntityParameter parameter, bool trackChanges)
        {
            PagedList<Partner> partners = await _partnerManagerRepository.RepositoryPartner.GetAllPaging(trackChanges, parameter);
            IEnumerable<PartnerDTO> partnersDTO = partners.Adapt<IEnumerable<PartnerDTO>>();
            PaginationDTO<PartnerDTO> pagination = new(partners.TotalPages, partners.CurrentPage, partnersDTO.ToList());
            return pagination;
        }

        public async Task<PartnerDTO> GetByIdAsync(int id, bool trackChanges)
        {
            Partner partner = await _partnerManagerRepository.RepositoryPartner.GetEntityById(id, true);
            PartnerDTO partnerDto = partner.Adapt<PartnerDTO>();
            return partnerDto;
        }

        public async Task UpdateAsync(int id, PartnerDTO entity)
        {
            Partner partner = await _partnerManagerRepository.RepositoryPartner.GetEntityById(id, true);
            partner.PartAccountNo = entity.PartAccountNo;
            partner.PartAddress = entity.PartAddress;
            partner.PartNpwp = entity.PartNpwp;
            partner.PartName = entity.PartName;
            partner.PartCityId = entity.PartCityId;
            partner.PartStatus = entity.PartStatus.ToString();
            partner.PartModifiedDate = DateTime.Now;
            await _partnerManagerRepository.UnitOfWorks.SaveChangesAsync();
        }

        public async Task<PartnerDTO> UpdateReturnAsync(int id, PartnerDTO entity)
        {
            Partner partner = await _partnerManagerRepository.RepositoryPartner.GetEntityById(id, true);
            partner.PartAccountNo = entity.PartAccountNo;
            partner.PartAddress = entity.PartAddress;
            partner.PartNpwp = entity.PartNpwp;
            partner.PartName = entity.PartName;
            partner.PartCityId = entity.PartCityId;
            partner.PartStatus = entity.PartStatus.ToString();
            partner.PartModifiedDate = DateTime.Now;
            await _partnerManagerRepository.UnitOfWorks.SaveChangesAsync();
            City city = await _partnerManagerRepository.RepositoryCity.GetEntityById(entity.PartCityId, false);
            PartnerDTO response = entity with
            {
                PartEntityid = entity.PartEntityid,
                CityName = city.CityName,
                PartCityId = city.CityId,
            };

            return response;
        }

    }
}
