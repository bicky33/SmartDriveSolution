using Domain.Enum;
using Domain.Repositories.SO;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Persistence.Repositories.SO
{
    public class UnitOfWorksSO : IUnitOfWorksSO
    {
        private readonly SmartDriveContext _dbContext;

        public UnitOfWorksSO(SmartDriveContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> GenerateSeroId(EnumModuleServiceOrder.SERVTYPE serviceType)
        {

            // get filter key
            var key = "";
            switch (serviceType)
            {
                case EnumModuleServiceOrder.SERVTYPE.FEASIBILITY:
                    key = "FS";
                    break;
                case EnumModuleServiceOrder.SERVTYPE.POLIS:
                    key = "PL";
                    break;
                case EnumModuleServiceOrder.SERVTYPE.CLAIM:
                    key = "CL";
                    break;
            }
            // ambil seroId terakhir berdasarkan service type
            var seroEntity = await _dbContext.ServiceOrders.Include(so => so.SeroServ).Where(c => c.SeroId.StartsWith(key)).OrderBy(o => o.SeroId).LastOrDefaultAsync();

            // check if not found
            if (seroEntity == null)
            {
                key += $"0001-{DateTime.Now.ToString("yyyyMMdd")}";
                return key;
            }
            else
            {
                // ambil counter terakhir dari seroEntityId
                var substring = seroEntity.SeroId.Substring(2, 4).SkipWhile(item => item == '0');
                // looping through substring 
                var newSubstring = "";
                foreach (var item in substring)
                {
                    newSubstring += item;
                }
                // add to counter
                var counter = Int32.Parse(newSubstring);

                // add counter
                counter++;

                // construct seroId
                string seroId = "";

                // add service type
                seroId += key;

                // add counter
                seroId += counter.ToString().PadLeft(4, '0');

                // add strip
                seroId += "-";

                // add date
                seroId += DateTime.Today.ToString("yyyyMMdd");
                return seroId;
            }

        }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                var res = await _dbContext.SaveChangesAsync();
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> GetAgentAreaWorkgroup(int agentId)
        {
            var agentEntity = await _dbContext.EmployeeAreWorkgroups.Where(c => c.EawgId.Equals(agentId)).FirstAsync();
            if (agentEntity.EawgArwgCode.IsNullOrEmpty())
                return "";
            return agentEntity.EawgArwgCode!.ToString();
        }
        public void DisableTracking()
        {
            _dbContext.ChangeTracker.Clear();
            _dbContext.ChangeTracker.AutoDetectChangesEnabled = false;
            var entryentity = _dbContext.ChangeTracker.Entries();

        }

        public async Task<string> GenerateInsuranceNo()
        {
            // get all existing service polis with today datetime
            // if null counter set to 1
            var service = await _dbContext.Services.Where(c => c.ServType == EnumModuleServiceOrder.SERVTYPE.POLIS.ToString() && (c.ServInsuranceNo != null)).OrderBy(c => c.ServInsuranceNo).Select(c => c.ServInsuranceNo).ToListAsync();
            var filteredService = service.Where(c => c.Contains("512-" + DateTime.Now.ToString("ddMMyy")));
            if (filteredService.Count() == 0) return "512-" + DateTime.Now.ToString("ddMMyy") + "01";
            var strCounter = service.Last()!.Substring(service.Last()!.Length - 2).SkipWhile(item => item == '0');
            // looping through substring 
            var newSubstring = "";
            foreach (var item in strCounter)
            {
                newSubstring += item;
            }
            var counter = Int32.Parse(newSubstring);
            counter++;
            return "512-" + DateTime.Now.ToString("ddMMyy") + counter.ToString().PadLeft(2, '0'); ;
        }
    }
}
