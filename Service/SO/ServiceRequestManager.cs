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
    public class ServiceRequestManager : IServiceRequestManager
    {
        private readonly IServiceRequestBase _serviceRequest;
        public ServiceRequestManager(IRepositoryManager repositoryManager,IServiceManager serviceManager)
        {
            _serviceRequest = new ServiceRequest(repositoryManager,serviceManager);
        }

        public IServiceRequestBase ServiceRequest => _serviceRequest;
    }
}
