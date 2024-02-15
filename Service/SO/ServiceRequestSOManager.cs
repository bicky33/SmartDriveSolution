using Contract.DTO.SO;
using Domain.Repositories.SO;
using Service.SO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Abstraction.SO;
using ServiceOrderTask.SO;


namespace Service.Base
{
    public class ServiceRequestSOManager : IServiceRequestSOManager
    {
        private readonly IServiceRequestSOBase _serviceRequest;
        public ServiceRequestSOManager(IRepositorySOManager repositoryManager,IServiceSOManager serviceManager)
        {
            _serviceRequest = new ServiceRequest(repositoryManager,serviceManager);
        }

        public IServiceRequestSOBase ServiceRequest => _serviceRequest;
    }
}
