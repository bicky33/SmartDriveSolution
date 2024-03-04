using Contract.DTO.Partners;
using Contract.DTO.UserModule;
using Contract.Records;
using Domain.Entities.Partners;
using Domain.Entities.Users;
using Domain.Enum;
using Domain.Repositories.Partners;
using Domain.RequestFeatured;
using Mapster;
using Service.Abstraction.Base;
using Service.Abstraction.Partners;
using Service.Abstraction.User;
using System.Transactions;

namespace Service.Partners
{
    public class ServicePartnerContact : IServicePartnerContact
    {
        private readonly IRepositoryPartnerManager _repositoryPartnerManager;
        public ServicePartnerContact(IRepositoryPartnerManager repositoryPartnerMannager)
        {
            _repositoryPartnerManager = repositoryPartnerMannager;
        }

        public async Task<PartnerContactDTO> CreateAsync(PartnerContactDTO entity)
        {
            using var transaction = new TransactionScope(
                TransactionScopeOption.Required, 
                TransactionScopeAsyncFlowOption.Enabled
             );
            try
            {
                string statusRoles = entity.IsGranted ? PartnerStatus.ACTIVE.ToString() : PartnerStatus.INACTIVE.ToString();
                BusinessEntity businessEntity = _repositoryPartnerManager.RepositoryBusinessEntity.CreateEntity();
                await _repositoryPartnerManager.UnitOfWorks.SaveChangesAsync();
                User user = new()
                {
                    UserEntityid = businessEntity.Entityid,
                    UserName = entity.FullName,
                    UserFullName = entity.FullName,
                    UserNpwp = entity.PhoneNumber,
                    UserEmail = entity.PhoneNumber,
                    UserNationalId = entity.PhoneNumber,
                    UserPassword = entity.PhoneNumber,
                    UserModifiedDate = DateTime.Now,
                    UserBirthDate = DateTime.Now,
                };
                 _repositoryPartnerManager.RepositoryUser.CreateEntity(user);
                await _repositoryPartnerManager.UnitOfWorks.SaveChangesAsync();
                UserRole userRoles = new() { 
                    UsroEntityid = businessEntity.Entityid, 
                    UsroRoleName = EnumRoleType.PR.ToString(), 
                    UsroStatus = statusRoles 
                };
                _repositoryPartnerManager.RepositoryUserRole.CreateEntity(userRoles);
                await _repositoryPartnerManager.UnitOfWorks.SaveChangesAsync();
                UserPhone userPhone = new()
                {
                    UsphEntityid = businessEntity.Entityid,
                    UsphPhoneNumber = entity.PhoneNumber,
                    UsphPhoneType = UserPhoneType.HP.ToString()
                };
                _repositoryPartnerManager.RepositoryUserPhone.CreateEntity(userPhone);
                PartnerContact partnerContact = new()
                {
                    PacoPatrnEntityid = entity.PacoPatrnEntityid,
                    PacoUserEntityid = businessEntity.Entityid,
                    PacoStatus = statusRoles,
                };
                Partner partner = await _repositoryPartnerManager.RepositoryPartner.GetEntityById(entity.PacoPatrnEntityid, false);
                PartnerContactDTO response = partnerContact.Adapt<PartnerContactDTO>() with
                {
                    PacoPatrnEntityName = partner.PartName,
                };
                 _repositoryPartnerManager.RepositoryPartnerContact.CreateEntity(partnerContact);
                await _repositoryPartnerManager.UnitOfWorks.SaveChangesAsync();
                transaction.Complete();
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(int pacoPatrnEntityid, int pacoUserEntityid)
        {
            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                PartnerContact partnerContact = await _repositoryPartnerManager.RepositoryPartnerContact.GetEntityById(pacoPatrnEntityid, pacoUserEntityid, true);
                _repositoryPartnerManager.RepositoryPartnerContact.DeleteEntity(partnerContact);
                await _repositoryPartnerManager.UnitOfWorks.SaveChangesAsync();

                IEnumerable<UserPhone> userPhones = await _repositoryPartnerManager.RepositoryUserPhone.GetAllEntityById(pacoUserEntityid, true);
                _repositoryPartnerManager.RepositoryUserPhone.DeleteEntity(userPhones.First());
                await _repositoryPartnerManager.UnitOfWorks.SaveChangesAsync();

                UserRole userRole = await _repositoryPartnerManager.RepositoryUserRole.GetSingleUserRoleByIdAndUserRole(pacoUserEntityid, EnumRoleType.PR.ToString(), true);
                _repositoryPartnerManager.RepositoryUserRole.DeleteEntity(userRole);
                await _repositoryPartnerManager.UnitOfWorks.SaveChangesAsync();

                User user = await _repositoryPartnerManager.RepositoryUser.GetEntityById(pacoUserEntityid, true);
                _repositoryPartnerManager.RepositoryUser.DeleteEntity(user);
                await _repositoryPartnerManager.UnitOfWorks.SaveChangesAsync();

                transaction.Complete();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<PartnerContactDTO>> GetAllAsync(bool trackChanges)
        {
            IEnumerable<PartnerContact>  partnerContacts =  await _repositoryPartnerManager.RepositoryPartnerContact.GetAllEntity(trackChanges);
            IEnumerable<PartnerContactDTO> partnerContactDTO = partnerContacts.Adapt<IEnumerable<PartnerContactDTO>>();
            return partnerContactDTO;
        }

        public async Task<PaginationDTO<PartnerContactDTO>> GetAllPagingAsync(EntityParameter parameter)
        {
            PagedList<PartnerContact> partnerContact = await _repositoryPartnerManager.RepositoryPartnerContact.GetAllPagingAsync(false, parameter);
            IEnumerable<PartnerContactDTO> partnerContactDTO = partnerContact.Adapt<IEnumerable<PartnerContactDTO>>();
            PaginationDTO<PartnerContactDTO> pagination = new(partnerContact.TotalPages, partnerContact.CurrentPage, partnerContactDTO.ToList());
            return pagination;
        }
        public async Task<PartnerContactDTO> GetByIdAsync(int pacoPatrnEntityid, int pacoUserEntityid, bool trackChanges)
        {
            PartnerContact partnerContact = await _repositoryPartnerManager.RepositoryPartnerContact.GetEntityById(pacoPatrnEntityid, pacoUserEntityid, trackChanges);
            PartnerContactDTO partnerContactDTO = partnerContact.Adapt<PartnerContactDTO>();
            return partnerContactDTO;
        }

        public async Task UpdateAsync(int pacoPatrnEntityid, int pacoUserEntityid, PartnerContactDTO entity)
        {
            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                Dictionary<bool, string> status = new()
                {
                    { true, PartnerStatus.ACTIVE.ToString() },
                    { false, PartnerStatus.INACTIVE.ToString() }
                };

                PartnerContact partnerContact = await _repositoryPartnerManager.RepositoryPartnerContact.GetEntityById(pacoPatrnEntityid, pacoUserEntityid, true);
                User user = await _repositoryPartnerManager.RepositoryUser.GetEntityById(pacoUserEntityid, true);
                UserRole userRole = await _repositoryPartnerManager.RepositoryUserRole.GetSingleUserRoleByIdAndUserRole(pacoUserEntityid, EnumRoleType.PR.ToString(), true);
                IEnumerable<UserPhone> userPhones = await _repositoryPartnerManager.RepositoryUserPhone.GetAllEntityById(pacoUserEntityid, true);
                UserPhone userPhone = userPhones.First(c => c.UsphPhoneType.Equals(UserPhoneType.HP.ToString()));

                if (status[entity.IsGranted] != partnerContact.PacoStatus)
                {
                    partnerContact.PacoStatus = status[entity.IsGranted];
                    userRole.UsroStatus = status[entity.IsGranted];
                }

                if (entity.PhoneNumber != userPhone.UsphPhoneNumber)
                {
                    _repositoryPartnerManager.RepositoryUserPhone.DeleteEntity(userPhone);
                    await _repositoryPartnerManager.UnitOfWorks.SaveChangesAsync();
                    userPhone.UsphPhoneNumber = entity.PhoneNumber;
                    _repositoryPartnerManager.RepositoryUserPhone.CreateEntity(userPhone);
                    await _repositoryPartnerManager.UnitOfWorks.SaveChangesAsync();
                }

                if (entity.FullName != user.UserFullName)
                {
                    user.UserFullName = entity.FullName;
                    user.UserName = entity.FullName;
                    user.UserNpwp = entity.PhoneNumber;
                    user.UserPassword = BCrypt.Net.BCrypt.HashPassword(entity.PhoneNumber);
                }
                await _repositoryPartnerManager.UnitOfWorks.SaveChangesAsync();
                transaction.Complete();
            }
            catch (Exception)
            {
                throw;
            }



        }
    }
}
