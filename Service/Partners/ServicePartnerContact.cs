using Contract.DTO.Partners;
using Contract.DTO.UserModule;
using Domain.Entities.Partners;
using Domain.Entities.Users;
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
using System.Transactions;

namespace Service.Partners
{
    public class ServicePartnerContact : IServicePartnerContact
    {
        private readonly IRepositoryPartnerManager _repositoryPartnerMannager;
        private readonly Lazy<IServiceEntityBase<UserDto>> _serviceUser;
        public ServicePartnerContact(IRepositoryPartnerManager repositoryPartnerMannager, Lazy<IServiceEntityBase<UserDto>> serviceUser)
        {
            _repositoryPartnerMannager = repositoryPartnerMannager;
            _serviceUser = serviceUser;
        }

        public async Task<PartnerContactDTO> CreateAsync(PartnerContactDTO entity)
        {
            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                UserDto userDTO = new()
                {
                    UserName = entity.FullName,
                    UserFullName = entity.FullName,
                    UserNpwp = entity.PhoneNumber,
                    UserNationalId = entity.PhoneNumber,
                    UserPassword = entity.IsGranted ? entity.PhoneNumber : null,
                };
                UserDto userResult = await _serviceUser.Value.CreateAsync(userDTO);
                PartnerContact partnerContact = new()
                {
                    PacoPatrnEntityid = entity.PacoPatrnEntityid,
                    PacoUserEntityid = userResult.UserEntityid,
                    PacoStatus = entity.PacoStatus.ToString(),
                };
                 _repositoryPartnerMannager.RepositoryPartnerContact.CreateEntity(partnerContact);
                await _repositoryPartnerMannager.UnitOfWorks.SaveChangesAsync();
                transaction.Complete();
                return partnerContact.Adapt<PartnerContactDTO>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(int pacoPatrnEntityid, int pacoUserEntityid)
        {
            PartnerContact partnerContact = await _repositoryPartnerMannager.RepositoryPartnerContact.GetEntityById(pacoPatrnEntityid, pacoUserEntityid, true);
            _repositoryPartnerMannager.RepositoryPartnerContact.DeleteEntity(partnerContact);
            await _repositoryPartnerMannager.UnitOfWorks.SaveChangesAsync();
        }

        public async Task<IEnumerable<PartnerContactDTO>> GetAllAsync(bool trackChanges)
        {
            IEnumerable<PartnerContact>  partnerContacts =  await _repositoryPartnerMannager.RepositoryPartnerContact.GetAllEntity(trackChanges);
            IEnumerable<PartnerContactDTO> partnerContactDTO = partnerContacts.Adapt<IEnumerable<PartnerContactDTO>>();
            return partnerContactDTO;
        }

        public async Task<IEnumerable<PartnerContactDTO>> GetAllPagingAsync(EntityParameter parameter)
        {
            PagedList<PartnerContact> partnerContact = await _repositoryPartnerMannager.RepositoryPartnerContact.GetAllPagingAsync(false, parameter);
            IEnumerable<PartnerContactDTO> partnerContactDTO = partnerContact.Adapt<IEnumerable<PartnerContactDTO>>();
            return partnerContactDTO;
        }
        public async Task<PartnerContactDTO> GetByIdAsync(int pacoPatrnEntityid, int pacoUserEntityid, bool trackChanges)
        {
            PartnerContact partnerContact = await _repositoryPartnerMannager.RepositoryPartnerContact.GetEntityById(pacoPatrnEntityid, pacoUserEntityid, trackChanges);
            PartnerContactDTO partnerContactDTO = partnerContact.Adapt<PartnerContactDTO>();
            return partnerContactDTO;
        }

        public Task UpdateAsync(int id, PartnerContactDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
