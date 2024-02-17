using Contract.DTO.Partners;
using Contract.DTO.UserModule;
using Domain.Repositories.Base;
using Domain.Repositories.Partners;
using Domain.Repositories.UserModule;
using Service.Abstraction.Base;
using Service.Abstraction.Partners;
using Service.UserModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Partners
{
    public class ServicePartnerManager : IServicePartnerManager
    {
        private readonly Lazy<IServicePartner> _servicePartner;
        private readonly Lazy<IServicePartnerAreaWorkgroup> _servicePartnerAreaWorkgroup;
        private readonly Lazy<IServicePartnerContact> _servicePartnerContact;
        private readonly Lazy<IServiceEntityBase<UserDto>> _serviceUser;

        public ServicePartnerManager(IRepositoryPartnerManager _repositoryPartnerManager, IRepositoryManagerUser _repositoryUserManager)
        {
            _serviceUser = new Lazy<IServiceEntityBase<UserDto>>(() => new UserService(_repositoryUserManager));
            _servicePartner = new Lazy<IServicePartner>(() => new ServicePartner(_repositoryPartnerManager));
            _servicePartnerAreaWorkgroup = new Lazy<IServicePartnerAreaWorkgroup>(() => new ServicePartnerAreaWorkgroup(_repositoryPartnerManager));
            _servicePartnerContact = new Lazy<IServicePartnerContact>(() => new ServicePartnerContact(_repositoryPartnerManager, _serviceUser));
        }

        public IServicePartner ServicePartner => _servicePartner.Value;
        public IServicePartnerAreaWorkgroup ServicePartnerAreaWorkgroup => _servicePartnerAreaWorkgroup.Value;
        public IServicePartnerContact ServicePartnerContact => _servicePartnerContact.Value;
    }
}
