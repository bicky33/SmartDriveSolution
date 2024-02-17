using Contract.DTO.SO;
using Domain.Repositories.SO;
using Service.SO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Abstraction.SO;
using Service.Abstraction.Master;


namespace Service.SO
{
    public class ServiceRequestSOManager : IServiceRequestSOManager
    {
        private readonly IServiceRequestSOBase _serviceRequest;
        public ServiceRequestSOManager(IRepositorySOManager repositoryManager,IServiceSOManager serviceManager,IServiceManagerMaster serviceManagerMaster)
        {
            _serviceRequest = new ServiceRequest(repositoryManager,serviceManager, serviceManagerMaster);
        }

        public IServiceRequestSOBase ServiceRequest => _serviceRequest;
    }
}
