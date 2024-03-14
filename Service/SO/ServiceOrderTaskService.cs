using Contract.DTO.SO;
using Domain.Enum;
using Domain.Exceptions;
using Domain.Repositories.SO;
using Mapster;
using Service.Abstraction.HR;
using Service.Abstraction.Master;
using Service.Abstraction.SO;
using Service.Abstraction.User;
using System.Reflection;

namespace Service.SO
{
    public class ServiceOrderTaskService : IServiceSOEntityBase<ServiceOrderTaskDto, ServiceOrderTaskDtoCreate, int>
    {
        private readonly IRepositorySOManager _repositoryManager;
        private readonly IServiceManagerMaster _serviceManagerMaster;
        private readonly IServiceManagerUser _serviceUserManager;
        private readonly IMailService _mailService;
        private readonly IServiceHRManager _serviceHRManager;

        public ServiceOrderTaskService(IRepositorySOManager repositoryManager, IServiceManagerMaster serviceManagerMaster, IServiceManagerUser serviceUserManager, IMailService mailService, IServiceHRManager serviceHRManager)
        {
            _repositoryManager = repositoryManager;
            _serviceManagerMaster = serviceManagerMaster;
            _serviceUserManager = serviceUserManager;
            _mailService = mailService;
            _serviceHRManager = serviceHRManager;
        }

        public async Task<ServiceOrderTaskDtoCreate> CreateAsync(ServiceOrderTaskDtoCreate entity)
        {
            var service = entity.Adapt<Domain.Entities.SO.ServiceOrderTask>();
            _repositoryManager.ServiceOrderTaskRepository.CreateEntity(service);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();
            return service.Adapt<ServiceOrderTaskDtoCreate>();
        }

        public async Task DeleteAsync(int id)
        {
            var service = await _repositoryManager.ServiceOrderTaskRepository.GetEntityById(id, false);
            if (service == null)
                throw new EntityNotFoundException(id, "ServiceOrderTask");
            _repositoryManager.ServiceOrderTaskRepository.DeleteEntity(service);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<ServiceOrderTaskDto>> GetAllAsync(bool trackChanges)
        {
            var services = await _repositoryManager.ServiceOrderTaskRepository.GetAllEntity(trackChanges);
            var serviceDtos = services.Adapt<IEnumerable<ServiceOrderTaskDto>>();
            return serviceDtos;
        }

        public async Task<ServiceOrderTaskDto> GetByIdAsync(int id, bool trackChanges)
        {
            var task = await _repositoryManager.ServiceOrderTaskRepository.GetEntityById(id, trackChanges);
            if (task == null)
                throw new EntityNotFoundException(id, "ServiceOrderTask");
            var taskDto = task.Adapt<ServiceOrderTaskDto>();
            taskDto.Sowos = task.ServiceOrderWorkorders.Adapt<ICollection<ServiceOrderWorkorderDto>>().Select(c => new ServiceOrderWorkorderDto
            {
                SowoId = c.SowoId,
                SowoModifiedDate = c.SowoModifiedDate,
                SowoName = c.SowoName,
                SowoStatus = c.SowoStatus,
            }).ToList();
            return taskDto;
        }

        public async Task<ServiceOrderTaskDtoCreate> UpdateAsync(int id, ServiceOrderTaskDtoCreate entity)
        {
            var task = await _repositoryManager.ServiceOrderTaskRepository.GetEntityById(id, true);
            if (task == null)
                throw new EntityNotFoundException(id, "ServiceOrderTask");

            task.SeotId = id;
            task.SeotName = entity.SeotName is not null ? entity.SeotName : task.SeotName;
            task.SeotStartdate = entity.SeotStartdate is not null ? entity.SeotStartdate : task.SeotStartdate;
            task.SeotEnddate = entity.SeotEnddate is not null ? entity.SeotEnddate : task.SeotEnddate;
            task.SeotActualStartdate = entity.SeotActualStartdate is not null ? entity.SeotActualStartdate : task.SeotActualStartdate;
            task.SeotActualEnddate = entity.SeotActualEnddate is not null ? entity.SeotActualEnddate : task.SeotActualEnddate;
            task.SeotStatus = entity.SeotStatus is not null ? entity.SeotStatus : task.SeotStatus;
            task.SeotArwgCode = entity.SeotArwgCode is not null ? entity.SeotArwgCode : task.SeotArwgCode;
            task.SeotSeroId = entity.SeotSeroId is not null ? entity.SeotSeroId : task.SeotSeroId;

            await _repositoryManager.UnitOfWork.SaveChangesAsync();

            // check legal document status 
            // if legal document status completed, generate polis
            if (task.SeotName!.Equals("LEGAL DOCUMENT SIGNED") && task.SeotStatus!.Equals(EnumModuleServiceOrder.SEOTSTATUS.COMPLETED.ToString()))
            {
                // fetch neccesary data
                var newTask = await _repositoryManager.ServiceOrderTaskRepository.GetEntityById(task.SeotId, false);
                var serv = await _repositoryManager.ServiceRepository.GetEntityById((int)newTask.SeotSero!.SeroServId!, false);

                // get dto for createservicepolis
                var dto = new CreateServicePolisDto
                {
                    AgentId = (int)newTask.SeotSero!.SeroAgentEntityid!,
                    ServId = serv.ServId,
                    CreatePolisDate = (DateTime)serv.ServCreatedOn!,
                    PolisStartDate = (DateTime)serv.ServStartdate!,
                    PolisEndDate = (DateTime)serv.ServEnddate!,

                };
                // reflection to invoke method
                Type type = typeof(ServiceService);
                object instance = Activator.CreateInstance(type, new object[]
                {
                    _repositoryManager,
                    _serviceManagerMaster,
                    _mailService,
                    _serviceUserManager,
                    _serviceHRManager,
                })!;
                MethodInfo method = type.GetMethod("CreateServicePolis")!;
                var service = method.Invoke(instance, new object[] { dto });
                await (Task)service;
                var result = (ServiceDto)service.GetType().GetProperty("Result").GetValue(service, null);
            }
            return task.Adapt<ServiceOrderTaskDtoCreate>();
        }
    }
    public static class TypeExtensions
    {
        public static PropertyInfo[] GetPublicProperties(this Type targetType)
        {
            return targetType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }
    }
}
