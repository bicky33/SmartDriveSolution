using Contract.DTO.UserModule;
using Domain.Entities.Users;
using Domain.Repositories.UserModule;
using Mapster;
using Service.Abstraction.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.UserModule
{
    public class BusinessEntityService : IServiceBusinessEntity
    {
        private readonly IRepositoryManagerUser _repositoryManager;

        public BusinessEntityService(IRepositoryManagerUser repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }
        public async Task<BusinessEntity> CreateBusinessEntity()
        {
            var businessEntity = _repositoryManager.BusinessEntityRepository.CreateEntity();
            await _repositoryManager.UnitOfWork.SaveChangesAsync();

            return businessEntity;
        }
    }
}
