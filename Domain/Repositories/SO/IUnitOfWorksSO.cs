using Domain.Entities.HR;
using Domain.Enum;

namespace Domain.Repositories.SO
{
    public interface IUnitOfWorksSO
    {
        public Task<int> SaveChangesAsync();
        public Task<string> GenerateSeroId(EnumModuleServiceOrder.SERVTYPE serviceType);
        public string GenerateInsuranceNo();
        public Task<string> GetAgentAreaWorkgroup(int agentId);
        public void Debugging();
    }
}
