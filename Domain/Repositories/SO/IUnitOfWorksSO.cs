using Domain.Enum;

namespace Domain.Repositories.SO
{
    public interface IUnitOfWorksSO
    {
        public Task<int> SaveChangesAsync();
        public Task<string> GenerateSeroId(EnumModuleServiceOrder.SERVTYPE serviceType);
        public Task<string> GenerateInsuranceNo();
        public Task<string> GetAgentAreaWorkgroup(int agentId);
        public void DisableTracking();
    }
}
