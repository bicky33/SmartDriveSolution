using Contract.DTO.SO;
using Domain.Enum;
using Domain.Repositories.SO;
using Service.Abstraction.SO;

namespace Service.SO
{
    public class ServiceRequest :IServiceRequestSOBase
    {
        private readonly IRepositorySOManager _repositoryManager;
        private readonly IServiceSOManager _serviceManager;

        public ServiceRequest(IRepositorySOManager repositoryManager, IServiceSOManager serviceManager)
        {
            _repositoryManager = repositoryManager;
            _serviceManager = serviceManager;
        }

        public async Task CreateServicePolis(CreateServicePolisDto createServicePolisDto)
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
                ServStatus = EnumModuleServiceOrder.SERVSTATUS.INACTIVE.ToString()
            };
            var newServiceDtoCreate = await _serviceManager.ServiceService.CreateAsync(serviceDtoCreate);

            // create service order entity
            var serviceOrderDtoCreate = new ServiceOrderDtoCreate()
            {
                SeroId = await _repositoryManager.UnitOfWork.GenerateSeroId(EnumModuleServiceOrder.SERVTYPE.FEASIBILITY),
                SeroAgentEntityid = createServicePolisDto.AgentId,
                SeroOrdtType = EnumModuleServiceOrder.SEROORDTTYPE.CREATE.ToString(),
                SeroStatus = EnumModuleServiceOrder.SEROSTATUS.OPEN.ToString(),
                SeroServId = newServiceDtoCreate.ServId
            };
            var newServiceOrderDtoCreate = await _serviceManager.ServiceOrderService.CreateAsync(serviceOrderDtoCreate);

            // create service order task entity
            var templateTestaFeasibility = new List<string>()
            {
                "REVIEW & CHECK CUSTOMER REQUEST",
                "PROSPEK CUSTOMER POTENTIAL",
                "PREMI SCHEMA",
                "LEGAL DOCUMENT SIGNED",

            };
            List<List<string>> templateTewoNameFeasibility = new();
            templateTewoNameFeasibility.Add(new List<string>() { "CHECK UMUR", "RELATE GOVERNMENT" });
            templateTewoNameFeasibility.Add(new List<string>() { "Dummy" });
            templateTewoNameFeasibility.Add(new List<string>() { "PREMI SCHEMA", "PREMI SCHEMA" });
            templateTewoNameFeasibility.Add(new List<string>() { "Dummy" });

            // get area workgroup code
            var areaWorkgroupCode = await _repositoryManager.UnitOfWork.GetAgentAreaWorkgroup((int)serviceOrderDtoCreate.SeroAgentEntityid);
            // looping through template order task
            foreach (var task in templateTestaFeasibility.Select((value,index)=>new {value,index}))
            {
                var serviceOrderTaskDtoCreate = new ServiceOrderTaskDtoCreate()
                {
                    SeotName=task.value,
                    SeotStartdate=DateTime.Today,
                    SeotEnddate=DateTime.Today.AddDays(5),
                    SeotStatus=EnumModuleServiceOrder.SEOTSTATUS.INPROGRESS.ToString(),
                    SeotSeroId= newServiceOrderDtoCreate.SeroId,
                    SeotArwgCode= areaWorkgroupCode,
                };
                // add to task
                var newServiceOrderTaskDtoCreate = await _serviceManager.ServiceOrderTaskService.CreateAsync(serviceOrderTaskDtoCreate);

                // check if create operation has success

                // looping through template workorder
                foreach (var workorder in templateTewoNameFeasibility[task.index])
                {
                    var serviceOrderWorkorderDtoCreate = new ServiceOrderWorkorderDtoCreate()
                    {
                        SowoName = workorder,
                        SowoSeotId = newServiceOrderTaskDtoCreate.SeotId,
                        SowoStatus = "0",
                        SowoModifiedDate=DateTime.Today,
                    };

                    // add to workorder
                    await _serviceManager.ServiceOrderWorkorderService.CreateAsync(serviceOrderWorkorderDtoCreate);
                    
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
