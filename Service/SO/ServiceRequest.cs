using Contract.DTO.SO;
using Domain.Enum;
using Domain.Entities.SO;
using Domain.Repositories.SO;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Service.Abstraction.Master;
using Service.Abstraction.SO;
using Service.SO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Exceptions;
using Service.Base;
using Mapster;
using Domain.Exceptions.SO;

namespace Service.SO
{
    public class ServiceRequest : IServiceRequestSOBase
    {
        private readonly IRepositorySOManager _repositoryManager;
        private readonly IServiceSOManager _serviceSOManager;
        private readonly IServiceManagerMaster _serviceManagerMaster;

        public ServiceRequest(IRepositorySOManager repositoryManager, IServiceSOManager serviceSOManager, IServiceManagerMaster serviceManagerMaster)
        {
            _repositoryManager = repositoryManager;
            _serviceSOManager = serviceSOManager;
            _serviceManagerMaster = serviceManagerMaster;
        }

        // close all service with type polis
        public async Task ClosePolis(int servId, string reason)
        {
            // get service by id
            var serv=await _serviceSOManager.ServiceService.GetByIdAsync(servId, false);

            if(serv==null)
                throw new EntityNotFoundExceptionSO(servId,"Service");

            // update status of serv status 
            serv.ServStatus=EnumModuleServiceOrder.SERVSTATUS.INACTIVE.ToString();
            serv.ServType=EnumModuleServiceOrder.SERVTYPE.CLOSED.ToString();

            // convert to entity
            var servDto = serv.Adapt<ServiceDtoCreate>();

            // save changes
            await _serviceSOManager.ServiceService.UpdateAsync(servId, servDto);

            // get all sero that belongs to serv
            var allSero=await _serviceSOManager.ServiceOrderService.GetAllAsync(false);
            var seroDto=allSero.Where(c=>(c.SeroServId.Equals(servId)) && (c.SeroId.StartsWith("PL"))).FirstOrDefault();

            // if not found
            if (seroDto == null)
                throw new EntityNotFoundExceptionSO(servId,"Service Order");
            
            // get sero by servId
            seroDto=await _serviceSOManager.ServiceOrderService.GetByIdAsync(seroDto.SeroId, false);

            // update related service order
            seroDto.SeroReason = reason;
            seroDto.SeroStatus = EnumModuleServiceOrder.SEROSTATUS.CLOSED.ToString();

            // convert to entity
            var sero= seroDto.Adapt<ServiceOrderDtoCreate>();

            // save changes
            await _serviceSOManager.ServiceOrderService.UpdateAsync(sero.SeroId, sero);
        }

        // service with type claim
        public async Task CreateClaimPolis(CreateClaimPolisDto createClaimPolistDto)
        {
            // cari service dari dto 
            var serv=await _serviceSOManager.ServiceService.GetByIdAsync(createClaimPolistDto.ServId, false);

            // create service entity with claim type
            var serviceDtoCreate = new ServiceDtoCreate()
            {
                ServCreatedOn = createClaimPolistDto.CreateClaimDate,
                ServCreqEntityid = serv.ServCreqEntityid,
                ServCustEntityid = serv.ServCustEntityid,
                ServStartdate = createClaimPolistDto.ClaimStartDate,
                ServEnddate = createClaimPolistDto.ClaimEndDate,
                ServType = EnumModuleServiceOrder.SERVTYPE.CLAIM.ToString(),
                ServStatus = EnumModuleServiceOrder.SERVSTATUS.ACTIVE.ToString(),
                ServServId = serv.ServId
            };
            var newServiceDtoCreate = await _serviceSOManager.ServiceService.CreateAsync(serviceDtoCreate);

            // get sero by serv id
            var allSero = await _serviceSOManager.ServiceOrderService.GetAllAsync(false);
            var sero = allSero.Where(c => c.SeroServId.Equals(serv.ServId)).FirstOrDefault();

            // create service order entity
            var serviceOrderDtoCreate = new ServiceOrderDtoCreate()
            {
                SeroId = await _repositoryManager.UnitOfWork.GenerateSeroId(EnumModuleServiceOrder.SERVTYPE.CLAIM),
                SeroAgentEntityid = createClaimPolistDto.AgentId,
                SeroOrdtType = EnumModuleServiceOrder.SEROORDTTYPE.CREATE.ToString(),
                SeroStatus = EnumModuleServiceOrder.SEROSTATUS.OPEN.ToString(),
                SeroServId = newServiceDtoCreate.ServId,
                SeroSeroId=sero is not null? sero.SeroId : null
            };
            var newServiceOrderDtoCreate = await _serviceSOManager.ServiceOrderService.CreateAsync(serviceOrderDtoCreate);

            // create service order task entity
            var TemplateTestaFeasibility = await _serviceManagerMaster.TemplateServiceTaskService.GetAllTestaAsync(3, false);

            // get area workgroup code
            var areaWorkgroupCode = await _repositoryManager.UnitOfWork.GetAgentAreaWorkgroup((int)serviceOrderDtoCreate.SeroAgentEntityid);
            // looping through template order task
            foreach (var task in TemplateTestaFeasibility.Select((value, index) => new { value, index }))
            {
                var serviceOrderTaskDtoCreate = new ServiceOrderTaskDtoCreate()
                {
                    SeotName = task.value.TestaName,
                    SeotStartdate = DateTime.Today,
                    SeotEnddate = DateTime.Today.AddDays(5),
                    SeotStatus = EnumModuleServiceOrder.SEOTSTATUS.INPROGRESS.ToString(),
                    SeotSeroId = newServiceOrderDtoCreate.SeroId,
                    SeotArwgCode = areaWorkgroupCode,
                };
                // add to task
                var newServiceOrderTaskDtoCreate = await _serviceSOManager.ServiceOrderTaskService.CreateAsync(serviceOrderTaskDtoCreate);

                // check if create operation has success

                // looping through template workorder
                var template = await _serviceManagerMaster.TemplateTaskWorkorderService.GetAllAsync(false);
                var templateTaskWorkorder = template.Where(x => x.TewoTestaId.Equals(task.value.TestaId));
                foreach (var workorder in templateTaskWorkorder)
                {
                    var serviceOrderWorkorderDtoCreate = new ServiceOrderWorkorderDtoCreate()
                    {
                        SowoName = workorder.TewoName,
                        SowoSeotId = newServiceOrderTaskDtoCreate.SeotId,
                        SowoStatus = "0",
                        SowoModifiedDate = DateTime.Today,
                    };

                    // add to workorder
                    await _serviceSOManager.ServiceOrderWorkorderService.CreateAsync(serviceOrderWorkorderDtoCreate);

                    // check if create operation has success

                }
            }
        }

        // service with type feasibility
        public async Task CreateServicePolisFeasibility(CreateServicePolisFeasibilityDto createServicePolisDto)
        {
            // create service entity
            var serviceDtoCreate = new ServiceDtoCreate()
            {
                ServCreatedOn = createServicePolisDto.CreatePolisDate,
                ServCreqEntityid = createServicePolisDto.CreqId,
                ServCustEntityid = createServicePolisDto.CreqId,
                ServStartdate = createServicePolisDto.PolisStartDate,
                ServEnddate = createServicePolisDto.PolisEndDate,
                ServType = EnumModuleServiceOrder.SERVTYPE.FEASIBILITY.ToString(),
                ServStatus = EnumModuleServiceOrder.SERVSTATUS.ACTIVE.ToString()
            };
            var newServiceDtoCreate = await _serviceSOManager.ServiceService.CreateAsync(serviceDtoCreate);

            // create service order entity
            var serviceOrderDtoCreate = new ServiceOrderDtoCreate()
            {
                SeroId = await _repositoryManager.UnitOfWork.GenerateSeroId(EnumModuleServiceOrder.SERVTYPE.FEASIBILITY),
                SeroAgentEntityid = createServicePolisDto.AgentId,
                SeroOrdtType = EnumModuleServiceOrder.SEROORDTTYPE.CREATE.ToString(),
                SeroStatus = EnumModuleServiceOrder.SEROSTATUS.OPEN.ToString(),
                SeroServId = newServiceDtoCreate.ServId
            };
            var newServiceOrderDtoCreate = await _serviceSOManager.ServiceOrderService.CreateAsync(serviceOrderDtoCreate);

            // create service order task entity
            var TemplateTestaFeasibility = await _serviceManagerMaster.TemplateServiceTaskService.GetAllTestaAsync(1, false);

            // get area workgroup code
            var areaWorkgroupCode = await _repositoryManager.UnitOfWork.GetAgentAreaWorkgroup((int)serviceOrderDtoCreate.SeroAgentEntityid);
            // looping through template order task
            foreach (var task in TemplateTestaFeasibility.Select((value,index)=>new {value,index}))
            {
                var serviceOrderTaskDtoCreate = new ServiceOrderTaskDtoCreate()
                {
                    SeotName=task.value.TestaName,
                    SeotStartdate=DateTime.Today,
                    SeotEnddate=DateTime.Today.AddDays(5),
                    SeotStatus=EnumModuleServiceOrder.SEOTSTATUS.INPROGRESS.ToString(),
                    SeotSeroId= newServiceOrderDtoCreate.SeroId,
                    SeotArwgCode= areaWorkgroupCode,
                };
                // add to task
                var newServiceOrderTaskDtoCreate = await _serviceSOManager.ServiceOrderTaskService.CreateAsync(serviceOrderTaskDtoCreate);

                // check if create operation has success

                // looping through template workorder
                var template = await _serviceManagerMaster.TemplateTaskWorkorderService.GetAllAsync(false);
                var templateTaskWorkorder=template.Where(x => x.TewoTestaId.Equals(task.value.TestaId));
                foreach (var workorder in templateTaskWorkorder)
                {
                    var serviceOrderWorkorderDtoCreate = new ServiceOrderWorkorderDtoCreate()
                    {
                        SowoName = workorder.TewoName,
                        SowoSeotId = newServiceOrderTaskDtoCreate.SeotId,
                        SowoStatus = "0",
                        SowoModifiedDate=DateTime.Today,
                    };

                    // add to workorder
                    await _serviceSOManager.ServiceOrderWorkorderService.CreateAsync(serviceOrderWorkorderDtoCreate);
                    
                    // check if create operation has success

                }
            }
        }

        // service with type polis
        public async Task CreateServicePolis(CreateServicePolisDto createServicePolisDto)
        {
            // cari service dari dto 
            var serv = await _serviceSOManager.ServiceService.GetByIdAsync(createServicePolisDto.ServId, false);

            if (serv is null)
                throw new EntityNotFoundExceptionSO(createServicePolisDto.ServId, "Service");

            // create service entity with claim type
            var serviceDtoCreate = new ServiceDtoCreate()
            {
                ServCreatedOn = createServicePolisDto.CreatePolisDate,
                ServCreqEntityid = serv.ServCreqEntityid,
                ServCustEntityid = serv.ServCustEntityid,
                ServStartdate = createServicePolisDto.PolisStartDate,
                ServEnddate = createServicePolisDto.PolisEndDate,
                ServVehicleNo=serv.ServVehicleNo,
                ServType = EnumModuleServiceOrder.SERVTYPE.POLIS.ToString(),
                ServStatus = EnumModuleServiceOrder.SERVSTATUS.ACTIVE.ToString(),
                ServInsuranceNo = _repositoryManager.UnitOfWork.GenerateInsuranceNo(),
                ServServId = serv.ServId
            };
            var newServiceDtoCreate = await _serviceSOManager.ServiceService.CreateAsync(serviceDtoCreate);

            // get sero by serv id
            var allSero = await _serviceSOManager.ServiceOrderService.GetAllAsync(false);
            var sero=allSero.Where(c => c.SeroServId.Equals(serv.ServId)).FirstOrDefault();

            // check if null
            //if (sero == null)
            //    throw new EntityNotFoundExceptionSO(0, "Service Order");

            // create service order entity
            var serviceOrderDtoCreate = new ServiceOrderDtoCreate()
            {
                SeroId = await _repositoryManager.UnitOfWork.GenerateSeroId(EnumModuleServiceOrder.SERVTYPE.POLIS),
                SeroAgentEntityid = createServicePolisDto.AgentId,
                SeroOrdtType = EnumModuleServiceOrder.SEROORDTTYPE.CREATE.ToString(),
                SeroStatus = EnumModuleServiceOrder.SEROSTATUS.OPEN.ToString(),
                SeroServId = newServiceDtoCreate.ServId,
                SeroSeroId = sero is not null? sero.SeroId:null
            };
            var newServiceOrderDtoCreate = await _serviceSOManager.ServiceOrderService.CreateAsync(serviceOrderDtoCreate);

            // create service order task entity
            var TemplateTestaFeasibility = await _serviceManagerMaster.TemplateServiceTaskService.GetAllTestaAsync(3, false);

            // get area workgroup code
            var areaWorkgroupCode = await _repositoryManager.UnitOfWork.GetAgentAreaWorkgroup((int)serviceOrderDtoCreate.SeroAgentEntityid);
            // looping through template order task
            foreach (var task in TemplateTestaFeasibility.Select((value, index) => new { value, index }))
            {
                var serviceOrderTaskDtoCreate = new ServiceOrderTaskDtoCreate()
                {
                    SeotName = task.value.TestaName,
                    SeotStartdate = DateTime.Today,
                    SeotEnddate = DateTime.Today.AddDays(5),
                    SeotStatus = EnumModuleServiceOrder.SEOTSTATUS.INPROGRESS.ToString(),
                    SeotSeroId = newServiceOrderDtoCreate.SeroId,
                    SeotArwgCode = areaWorkgroupCode,
                };
                // add to task
                var newServiceOrderTaskDtoCreate = await _serviceSOManager.ServiceOrderTaskService.CreateAsync(serviceOrderTaskDtoCreate);

                // check if create operation has success

                // looping through template workorder
                var template = await _serviceManagerMaster.TemplateTaskWorkorderService.GetAllAsync(false);
                var templateTaskWorkorder = template.Where(x => x.TewoTestaId.Equals(task.value.TestaId));
                foreach (var workorder in templateTaskWorkorder)
                {
                    var serviceOrderWorkorderDtoCreate = new ServiceOrderWorkorderDtoCreate()
                    {
                        SowoName = workorder.TewoName,
                        SowoSeotId = newServiceOrderTaskDtoCreate.SeotId,
                        SowoStatus = "0",
                        SowoModifiedDate = DateTime.Today,
                    };

                    // add to workorder
                    await _serviceSOManager.ServiceOrderWorkorderService.CreateAsync(serviceOrderWorkorderDtoCreate);

                    // check if create operation has success

                }
            }
        }
        public void Debugging()
        {
            _repositoryManager.UnitOfWork.Debugging();
        }
    }
}
