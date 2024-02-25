using Contract.DTO.CR.Response;
using Contract.DTO.SO;
using Contract.DTO.UserModule;
using Domain.Enum;
using Domain.Exceptions;
using Domain.Exceptions.SO;
using Domain.Repositories.SO;
using Domain.Entities.SO;
using Mapster;
using Service.Abstraction.Master;
using Service.Abstraction.SO;
using Domain.Entities.Users;
using Contract.DTO.HR;
using Service.Abstraction.Payment;
using Contract.DTO.Payment;

namespace Service.SO
{
    public class ServiceService : IServiceSORelationBase<ServiceDto,ServiceDtoCreate,int>
    {
        private readonly IRepositorySOManager _repositoryManager;
        private readonly IServiceManagerMaster _serviceManagerMaster;
        private readonly IServicePaymentManager _servicePaymentManager;

        public ServiceService(IRepositorySOManager repositoryManager, IServiceManagerMaster serviceManagerMaster, IServicePaymentManager servicePaymentManager)
        {
            _repositoryManager = repositoryManager;
            _serviceManagerMaster = serviceManagerMaster;
            _servicePaymentManager = servicePaymentManager;
        }

        public async Task<ServiceDto> ClosePolis(int servId, string reason)
        {
            // get service by id
            var serv = await _repositoryManager.ServiceRepository.GetEntityById(servId, false);

            if (serv == null)
                throw new EntityNotFoundExceptionSO(servId, "Service");

            // update status of serv status 
            serv.ServStatus = EnumModuleServiceOrder.SERVSTATUS.INACTIVE.ToString();
            serv.ServType = EnumModuleServiceOrder.SERVTYPE.CLOSED.ToString();

            // save changes
            await _repositoryManager.UnitOfWork.SaveChangesAsync();

            // get all sero that belongs to serv
            var allSero = await _repositoryManager.ServiceOrderRepository.GetAllEntity(false);
            var seroDto = allSero.Where(c => (c.SeroServId.Equals(servId)) && (c.SeroId.StartsWith("PL"))).FirstOrDefault();

            // if not found
            if (seroDto == null)
                throw new EntityNotFoundExceptionSO(servId, "Service Order");

            // get sero by servId
            seroDto = await _repositoryManager.ServiceOrderRepository.GetEntityById(seroDto.SeroId, false);

            // update related service order
            seroDto.SeroReason = reason;
            seroDto.SeroStatus = EnumModuleServiceOrder.SEROSTATUS.CLOSED.ToString();

            // save changes
            await _repositoryManager.UnitOfWork.SaveChangesAsync();

            // retuurn close polis using getbyid
            var res= await _repositoryManager.ServiceRepository.GetEntityById(servId, false);
            return res.Adapt<ServiceDto>();
        }

        public async Task<ServiceDtoCreate> CreateAsync(ServiceDtoCreate entity)
        {
            var service = entity.Adapt<Domain.Entities.SO.Service>();
            _repositoryManager.ServiceRepository.CreateEntity(service);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();
            return service.Adapt<ServiceDtoCreate>();
        }

        public async Task<ServiceDto> CreateClaimPolis(CreateClaimPolisDto createClaimPolisDto)
        {
            // cari service dari dto 
            var serv = await _repositoryManager.ServiceRepository.GetEntityById(createClaimPolisDto.ServId, false);

            // create service entity with claim type
            var service = new Domain.Entities.SO.Service()
            {
                ServCreatedOn = createClaimPolisDto.CreateClaimDate,
                ServCreqEntityid = serv.ServCreqEntityid,
                ServCustEntityid = serv.ServCustEntityid,
                ServStartdate = createClaimPolisDto.ClaimStartDate,
                ServEnddate = createClaimPolisDto.ClaimEndDate,
                ServVehicleNo = serv.ServVehicleNo,
                ServType = EnumModuleServiceOrder.SERVTYPE.CLAIM.ToString(),
                ServStatus = EnumModuleServiceOrder.SERVSTATUS.ACTIVE.ToString(),
                ServServId = serv.ServId
            };

            // create serv entity
            _repositoryManager.ServiceRepository.CreateEntity(service);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();

            // get sero by serv id
            var allSero = await _repositoryManager.ServiceOrderRepository.GetAllEntity(false);
            var sero = allSero.Where(c => c.SeroServId.Equals(serv.ServId)).FirstOrDefault();

            // create service order entity
            var serviceOrder = new ServiceOrder()
            {
                SeroId = await _repositoryManager.UnitOfWork.GenerateSeroId(EnumModuleServiceOrder.SERVTYPE.CLAIM),
                SeroAgentEntityid = createClaimPolisDto.AgentId,
                SeroOrdtType = EnumModuleServiceOrder.SEROORDTTYPE.CREATE.ToString(),
                SeroStatus = EnumModuleServiceOrder.SEROSTATUS.OPEN.ToString(),
                SeroServId = service.ServId,
                SeroSeroId = sero is not null ? sero.SeroId : null
            };

            // create sero entity
            _repositoryManager.ServiceOrderRepository.CreateEntity(serviceOrder);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();

            // create service order task entity
            var TemplateTestaFeasibility = await _serviceManagerMaster.TemplateServiceTaskService.GetAllTestaAsync(3, false);

            // get area workgroup code
            var areaWorkgroupCode = await _repositoryManager.UnitOfWork.GetAgentAreaWorkgroup((int)serviceOrder.SeroAgentEntityid);
            // looping through template order task
            foreach (var task in TemplateTestaFeasibility.Select((value, index) => new { value, index }))
            {
                var serviceOrderTask = new ServiceOrderTask()
                {
                    SeotName = task.value.TestaName,
                    SeotStartdate = DateTime.Today,
                    SeotEnddate = DateTime.Today.AddDays(5),
                    SeotStatus = EnumModuleServiceOrder.SEOTSTATUS.INPROGRESS.ToString(),
                    SeotSeroId = serviceOrder.SeroId,
                    SeotArwgCode = areaWorkgroupCode,
                };
                // add to task
                _repositoryManager.ServiceOrderTaskRepository.CreateEntity(serviceOrderTask);
                await _repositoryManager.UnitOfWork.SaveChangesAsync();

                // looping through template workorder
                var template = await _serviceManagerMaster.TemplateTaskWorkorderService.GetAllAsync(false);
                var templateTaskWorkorder = template.Where(x => x.TewoTestaId.Equals(task.value.TestaId));
                foreach (var workorder in templateTaskWorkorder)
                {
                    var serviceOrderWorkorder = new ServiceOrderWorkorder()
                    {
                        SowoName = workorder.TewoName,
                        SowoSeotId = serviceOrderTask.SeotId,
                        // inprogress,cancel,completed
                        SowoStatus = EnumModuleServiceOrder.SEOTSTATUS.INPROGRESS.ToString(),
                        SowoModifiedDate = DateTime.Today,
                    };

                    // add to workorder
                    _repositoryManager.ServiceOrderWorkorderRepository.CreateEntity(serviceOrderWorkorder);
                    await _repositoryManager.UnitOfWork.SaveChangesAsync();
                }
            }
            return service.Adapt<ServiceDto>();
        }

        public async Task<ServiceDto> CreateServiceFeasibility(CreateServicePolisFeasibilityDto createServicePolisFeasibilityDto)
        {
            // create service entity
            var service = new Domain.Entities.SO.Service()
            {
                ServCreatedOn = createServicePolisFeasibilityDto.CreatePolisDate,
                ServCreqEntityid = createServicePolisFeasibilityDto.CreqId,
                ServCustEntityid = createServicePolisFeasibilityDto.CustId,
                ServStartdate = createServicePolisFeasibilityDto.PolisStartDate,
                ServEnddate = createServicePolisFeasibilityDto.PolisEndDate,
                ServVehicleNo = createServicePolisFeasibilityDto.ServVehicleNo,
                ServType = EnumModuleServiceOrder.SERVTYPE.FEASIBILITY.ToString(),
                ServStatus = EnumModuleServiceOrder.SERVSTATUS.ACTIVE.ToString()
            };
            _repositoryManager.ServiceRepository.CreateEntity(service);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();

            // create service order entity
            var serviceOrder = new ServiceOrder()
            {
                SeroId = await _repositoryManager.UnitOfWork.GenerateSeroId(EnumModuleServiceOrder.SERVTYPE.FEASIBILITY),
                SeroAgentEntityid = createServicePolisFeasibilityDto.AgentId,
                SeroOrdtType = EnumModuleServiceOrder.SEROORDTTYPE.CREATE.ToString(),
                SeroStatus = EnumModuleServiceOrder.SEROSTATUS.OPEN.ToString(),
                SeroServId = service.ServId
            };
            _repositoryManager.ServiceOrderRepository.CreateEntity(serviceOrder);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();

            // create service order task entity
            var TemplateTestaFeasibility = await _serviceManagerMaster.TemplateServiceTaskService.GetAllTestaAsync(1, false);

            // get area workgroup code
            var areaWorkgroupCode = await _repositoryManager.UnitOfWork.GetAgentAreaWorkgroup((int)serviceOrder.SeroAgentEntityid);
            // looping through template order task
            foreach (var task in TemplateTestaFeasibility.Select((value, index) => new { value, index }))
            {
                var serviceOrderTask = new ServiceOrderTask()
                {
                    SeotName = task.value.TestaName,
                    SeotStartdate = DateTime.Today,
                    SeotEnddate = DateTime.Today.AddDays(5),
                    SeotStatus = EnumModuleServiceOrder.SEOTSTATUS.INPROGRESS.ToString(),
                    SeotSeroId = serviceOrder.SeroId,
                    SeotArwgCode = areaWorkgroupCode,
                };
                // add to task
                _repositoryManager.ServiceOrderTaskRepository.CreateEntity(serviceOrderTask);
                await _repositoryManager.UnitOfWork.SaveChangesAsync();

                // looping through template workorder
                var template = await _serviceManagerMaster.TemplateTaskWorkorderService.GetAllAsync(false);
                var templateTaskWorkorder = template.Where(x => x.TewoTestaId.Equals(task.value.TestaId));
                foreach (var workorder in templateTaskWorkorder)
                {
                    var serviceOrderWorkorder = new ServiceOrderWorkorder()
                    {
                        SowoName = workorder.TewoName,
                        SowoSeotId = serviceOrderTask.SeotId,
                        // inprogress, cancel, completed
                        SowoStatus = EnumModuleServiceOrder.SEOTSTATUS.INPROGRESS.ToString(),
                        SowoModifiedDate = DateTime.Today,
                    };

                    // add to workorder
                    _repositoryManager.ServiceOrderWorkorderRepository.CreateEntity(serviceOrderWorkorder);
                    await _repositoryManager.UnitOfWork.SaveChangesAsync();

                }
            }
            // return service
            return service.Adapt<ServiceDto>();
        }

        public async Task<ServiceDto> CreateServicePolis(CreateServicePolisDto createServicePolisDto)
        {
            // cari service dari dto 
            var serv = await _repositoryManager.ServiceRepository.GetEntityById(createServicePolisDto.ServId, false);

            if (serv is null)
                throw new EntityNotFoundExceptionSO(createServicePolisDto.ServId, "Service");

            // create service entity with claim type
            var service = new Domain.Entities.SO.Service()
            {
                ServCreatedOn = createServicePolisDto.CreatePolisDate,
                ServCreqEntityid = serv.ServCreqEntityid,
                ServCustEntityid = serv.ServCustEntityid,
                ServStartdate = createServicePolisDto.PolisStartDate,
                ServEnddate = createServicePolisDto.PolisEndDate,
                ServVehicleNo = serv.ServVehicleNo,
                ServType = EnumModuleServiceOrder.SERVTYPE.POLIS.ToString(),
                ServStatus = EnumModuleServiceOrder.SERVSTATUS.ACTIVE.ToString(),
                ServInsuranceNo = _repositoryManager.UnitOfWork.GenerateInsuranceNo(),
                ServServId = serv.ServId
            };
            _repositoryManager.ServiceRepository.CreateEntity(service);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();

            // get sero by serv id
            var allSero = await _repositoryManager.ServiceOrderRepository.GetAllEntity(false);
            var sero = allSero.Where(c => c.SeroServId.Equals(serv.ServId)).FirstOrDefault();

            // create service order entity
            var serviceOrder = new ServiceOrder()
            {
                SeroId = await _repositoryManager.UnitOfWork.GenerateSeroId(EnumModuleServiceOrder.SERVTYPE.POLIS),
                SeroAgentEntityid = createServicePolisDto.AgentId,
                SeroOrdtType = EnumModuleServiceOrder.SEROORDTTYPE.CREATE.ToString(),
                SeroStatus = EnumModuleServiceOrder.SEROSTATUS.OPEN.ToString(),
                SeroServId = service.ServId,
                SeroSeroId = sero is not null ? sero.SeroId : null
            };
            _repositoryManager.ServiceOrderRepository.CreateEntity(serviceOrder);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();

            // create service order task entity
            var TemplateTestaFeasibility = await _serviceManagerMaster.TemplateServiceTaskService.GetAllTestaAsync(3, false);

            // get area workgroup code
            var areaWorkgroupCode = await _repositoryManager.UnitOfWork.GetAgentAreaWorkgroup((int)serviceOrder.SeroAgentEntityid);

            // looping through template order task
            foreach (var task in TemplateTestaFeasibility.Select((value, index) => new { value, index }))
            {
                var serviceOrderTask = new ServiceOrderTask()
                {
                    SeotName = task.value.TestaName,
                    SeotStartdate = DateTime.Today,
                    SeotEnddate = DateTime.Today.AddDays(5),
                    SeotStatus = EnumModuleServiceOrder.SEOTSTATUS.INPROGRESS.ToString(),
                    SeotSeroId = serviceOrder.SeroId,
                    SeotArwgCode = areaWorkgroupCode,
                };
                // add to task
                _repositoryManager.ServiceOrderTaskRepository.CreateEntity(serviceOrderTask);
                await _repositoryManager.UnitOfWork.SaveChangesAsync();

                // check if create operation has success

                // looping through template workorder
                var template = await _serviceManagerMaster.TemplateTaskWorkorderService.GetAllAsync(false);
                var templateTaskWorkorder = template.Where(x => x.TewoTestaId.Equals(task.value.TestaId));
                foreach (var workorder in templateTaskWorkorder)
                {
                    var serviceOrderWorkorder = new ServiceOrderWorkorder()
                    {
                        SowoName = workorder.TewoName,
                        SowoSeotId = serviceOrderTask.SeotId,
                        SowoStatus = EnumModuleServiceOrder.SEOTSTATUS.INPROGRESS.ToString(),
                        SowoModifiedDate = DateTime.Today,
                    };

                    // add to workorder
                    _repositoryManager.ServiceOrderWorkorderRepository.CreateEntity(serviceOrderWorkorder);
                    await _repositoryManager.UnitOfWork.SaveChangesAsync();

                }
            }

            // service premi
            var servicePremi = new ServicePremi()
            {
                SemiServId=service.ServId,
                SemiStatus=EnumModuleServiceOrder.SERVSTATUS.ACTIVE.ToString(),
                SemiModifiedDate=DateTime.Now,
                SemiPaidType=serv.ServCreqEntity!.CustomerInscAsset!.CiasPaidType,
                SemiPremiDebet=serv.ServCreqEntity!.CustomerInscAsset.CiasTotalPremi,
            };
            _repositoryManager.ServicePremiRepository.CreateEntity(servicePremi);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();

            // payment transaction


            // service premi credit
            if (servicePremi.SemiPaidType.Equals("CASH"))
            {
                

                var servicePremiCredit = new ServicePremiCredit()
                {
                    SecrServId = service.ServId,
                    SecrPremiDebet = servicePremi.SemiPremiDebet,
                    SecrYear = serv.ServCreqEntity.CustomerInscAsset.CiasYear,
                    SecrDuedate = DateTime.Today.AddDays(1),
                };
                _repositoryManager.ServicePremiCreditRepository.CreateEntity(servicePremiCredit);
                await _repositoryManager.UnitOfWork.SaveChangesAsync();
            }
            else
            {
                var creditTotal = servicePremi.SemiPremiDebet/12;
                var dueDate = DateTime.Today.AddDays(1);
                for (global::System.Int32 i = 0; i < 12; i++)
                {
                    var servicePremiCredit = new ServicePremiCredit()
                    {
                        SecrServId = service.ServId,
                        SecrPremiDebet = creditTotal,
                        SecrYear = serv.ServCreqEntity.CustomerInscAsset.CiasYear,
                        SecrDuedate = dueDate.AddMonths(i),
                    };
                    _repositoryManager.ServicePremiCreditRepository.CreateEntity(servicePremiCredit);
                    await _repositoryManager.UnitOfWork.SaveChangesAsync();
                }
            }

            // return service
            return service.Adapt<ServiceDto>();
        }

        public async Task DeleteAsync(int id)
        {
            var service = await _repositoryManager.ServiceRepository.GetEntityById(id,false);
            if (service == null)
                throw new EntityNotFoundException(id,"Service");
            _repositoryManager.ServiceRepository.DeleteEntity(service);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<ServiceDto>> GetAllAsync(bool trackChanges)
        {
            var services = await _repositoryManager.ServiceRepository.GetAllEntity(trackChanges);
            return services.Adapt<IEnumerable<ServiceDto>>();
        }

        public async Task<ServiceDto> GetByIdAsync(int id, bool trackChanges)
        {
            var service = await _repositoryManager.ServiceRepository.GetEntityById(id, trackChanges);
            if (service == null)
                throw new EntityNotFoundException(id,"Service");
            var serviceDtos = service.Adapt<ServiceDto>();
            
            // service order
            serviceDtos.Seros = service.ServiceOrders.Adapt<List<ServiceOrderDto>>();

            // customer
            serviceDtos.ServCustEntity = service.ServCustEntity.Adapt<UserDto>();
                
            // creq -> customer insc asset && (employee area workgroup -> employee)
            serviceDtos.ServCreqEntity = service.ServCreqEntity.Adapt<CustomerRequestDto>();
            if (service.ServCreqEntity is not null &&
                service.ServCreqEntity.CreqAgenEntity is not null &&
                service.ServCreqEntity.CreqAgenEntity.EawgEntity is not null)
                serviceDtos.ServCreqEntity.CreqAgenEntity!.EawgEntity = new EmployeeDto
                {
                    EmpName= service.ServCreqEntity.CreqAgenEntity.EawgEntity.EmpName
                };

            // service order task
            foreach(var sero in service.ServiceOrders.Select((value, index) => new { value, index }))
            {
                serviceDtos.Seros[sero.index].Seots = sero.value.ServiceOrderTasks.Adapt<List<ServiceOrderTaskDto>>().Select(c=>new ServiceOrderTaskDto
                {
                    SeotId=c.SeotId,
                    SeotName=c.SeotName,
                    SeotStatus=c.SeotStatus,
                    SeotStartdate=c.SeotStartdate,
                    SeotEnddate=c.SeotEnddate,
                    Sowos=c.Sowos,
                }).ToList();

                // service order workorder
                foreach(var seot in sero.value.ServiceOrderTasks.Select((value, index) => new { value, index }))
                {
                    serviceDtos.Seros[sero.index].Seots[seot.index].Sowos = seot.value.ServiceOrderWorkorders.Adapt<ICollection<ServiceOrderWorkorderDto>>().Select(c=>new ServiceOrderWorkorderDto
                    {
                        SowoId=c.SowoId,
                        SowoModifiedDate=c.SowoModifiedDate,
                        SowoName=c.SowoName,
                        SowoStatus=c.SowoStatus,
                    }).ToList();
                }
            }
            return serviceDtos;
        }

        public async Task<ServiceDto> SearchBySeroId(string seroId)
        {
            // get sero by seroid
            var sero = await _repositoryManager.ServiceOrderRepository.GetEntityById(seroId,false);
            // filter by sero.servId
            if (sero.SeroServId is null)
                throw new EntityBadRequestException("Entity with seroId not found");
            return await GetByIdAsync((int)sero.SeroServId, false);
        }

        public async Task<ServiceDtoCreate> UpdateAsync(int id, ServiceDtoCreate entity)
        {
            var services = await _repositoryManager.ServiceRepository.GetEntityById(id, true);
            if (services == null)
                throw new EntityNotFoundException(id,"Service");

            services.ServId = id;
            services.ServCreatedOn = entity.ServCreatedOn is not null ? entity.ServCreatedOn : services.ServCreatedOn;
            services.ServType= entity.ServType is not null ? entity.ServType : services.ServType;
            services.ServInsuranceNo=entity.ServInsuranceNo is not null ? entity.ServInsuranceNo : services.ServInsuranceNo;
            services.ServStatus=entity.ServStatus is not null ? entity.ServStatus : services.ServStatus;
            services.ServVehicleNo=entity.ServVehicleNo is not null ? entity.ServVehicleNo : services.ServVehicleNo;
            services.ServStartdate=entity.ServStartdate is not null ? entity.ServStartdate : services.ServStartdate;
            services.ServEnddate=entity.ServEnddate is not null ? entity.ServEnddate : services.ServEnddate;
            services.ServCustEntityid = entity.ServCustEntityid is not null ? entity.ServCustEntityid : services.ServCustEntityid;
            services.ServCreqEntityid = entity.ServCreqEntityid is not null ? entity.ServCreqEntityid : services.ServCreqEntityid;

            await _repositoryManager.UnitOfWork.SaveChangesAsync();
            return services.Adapt<ServiceDtoCreate>();
        }
    }
}
