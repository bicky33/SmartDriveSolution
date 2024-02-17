using Domain.Repositories.SO;
using Service.Abstraction.SO;


namespace Service.SO
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
