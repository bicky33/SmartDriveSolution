using Contract.DTO.Master;
using Domain.Entities.Master;
using Domain.Exceptions;
using Domain.Repositories.Master;
using Mapster;
using Service.Abstraction.Base;

namespace Service.Master
{
    public class InsuranceTypeService : IServiceEntityBaseMaster<InsuranceTypeResponse>
    {
        private readonly IRepositoryManagerMaster _repositoryManagerMaster;

        public InsuranceTypeService(IRepositoryManagerMaster repositoryManagerMaster)
        {
            _repositoryManagerMaster = repositoryManagerMaster;
        }

        public async Task<InsuranceTypeResponse> CreateAsyncMaster(InsuranceTypeResponse entity)
        {
            var insuranceType = entity.Adapt<InsuranceType>();
            _repositoryManagerMaster.InsuranceTypeRepository.CreateEntityMaster(insuranceType);
            await _repositoryManagerMaster.UnitOfWork.SaveChangesAsync();

            return insuranceType.Adapt<InsuranceTypeResponse>();
        }

        public async Task DeleteAsyncMaster(string name)
        {
            var insuranceType = await _repositoryManagerMaster.InsuranceTypeRepository.GetEntityByNameMaster(name, false);
            if (insuranceType == null)
            {
                //throw new EntityNotFoundException(name);
                throw new Exception($"Data {name} Not Found");
            }
            _repositoryManagerMaster.InsuranceTypeRepository.DeleteEntityMaster(insuranceType);
            await _repositoryManagerMaster.UnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<InsuranceTypeResponse>> GetAllAsyncMaster(bool trackChanges)
        {
            var insuranceTypes = await _repositoryManagerMaster.InsuranceTypeRepository.GetAllEntityMaster(false);
            var insuranceTypesResponse = insuranceTypes.Adapt<IEnumerable<InsuranceTypeResponse>>();
            return insuranceTypesResponse;
        }

        public async Task<InsuranceTypeResponse> GetByNameAsyncMaster(string name, bool trackChanges)
        {
            var insuranceType = await _repositoryManagerMaster.InsuranceTypeRepository.GetEntityByNameMaster(name, false);
            if (insuranceType == null)
            {
                //throw new EntityNotFoundException(name);
                throw new Exception($"Data {name} Not Found");
            }
            var insuranceTypeResponse = insuranceType.Adapt<InsuranceTypeResponse>();
            return insuranceTypeResponse;
        }

        public async Task<InsuranceTypeResponse> UpdateAsyncMaster(string name, InsuranceTypeResponse entity)
        {
            var insuranceType = await _repositoryManagerMaster.InsuranceTypeRepository.GetEntityByNameMaster(name, true);
            if (insuranceType == null)
            {
                //throw new EntityNotFoundException(id);
                throw new Exception($"Data {name} Not Found");
            }
            insuranceType.IntyName = entity.IntyName;
            insuranceType.IntyDesc = entity.IntyDesc;

            await _repositoryManagerMaster.UnitOfWork.SaveChangesAsync();
            return insuranceType.Adapt<InsuranceTypeResponse>();
        }
    }
}