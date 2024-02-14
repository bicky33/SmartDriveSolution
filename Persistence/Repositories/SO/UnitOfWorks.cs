using Domain.Entities.HR;
using Domain.Entities.SO;
using Domain.Enum;
using Domain.Repositories.SO;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Persistence.Repositories.SO
{
    public class UnitOfWorks : IUnitOfWorks
    {
        public static int SeroIdCounter { get; set; } = 0;
        private readonly SmartDriveContext _dbContext;

        public UnitOfWorks(SmartDriveContext dbContext)
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
            var seroEntity = await _dbContext.ServiceOrders.Include(so => so.SeroServ).Where(c=>c.SeroId.StartsWith(key)).OrderBy(o=>o.SeroId).LastAsync();
            // ambil counter terakhir dari seroEntityId
            var substring = seroEntity.SeroId.Substring(2,4).SkipWhile(item => item == '0');
            // looping through substring 
            var newSubstring = "" ;
            foreach(var item in substring)
            {
                newSubstring += item;
            }
            // add to counter
            var counter = Int32.Parse(newSubstring);

            // tambah counter
            counter++;

            // construct seroId
            string seroId="";

            // add service type
            seroId += key;
            //if (serviceType == EnumModuleServiceOrder.SERVTYPE.FEASIBILITY)
            //    seroId += "FS";
            //else if (serviceType == EnumModuleServiceOrder.SERVTYPE.POLIS)
            //    seroId += "PL";
            //else if(serviceType == EnumModuleServiceOrder.SERVTYPE.CLAIM)
            //    seroId += "CL";

            // add counter
            seroId += counter.ToString().PadLeft(4, '0');

            // add strip
            seroId += "-";

            // add date
            seroId += DateTime.Today.ToString("yyyyMMdd");
            return seroId;
        }

        public Task<int> SaveChangesAsync() =>
             _dbContext.SaveChangesAsync();

        public async Task<string> GetAgentAreaWorkgroup(int agentId)
        {
            var agentEntity=await _dbContext.EmployeeAreWorkgroups.Where(c => c.EawgId.Equals(agentId)).FirstAsync();
            if (agentEntity.EawgArwgCode.IsNullOrEmpty())
                return "";
            return agentEntity.EawgArwgCode.ToString();
        }
        public void Debugging()
        {

            EnumModuleServiceOrder.SERVTYPE serviceType = EnumModuleServiceOrder.SERVTYPE.FEASIBILITY;
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
            var includes = _dbContext.ServiceOrders.Include(so => so.SeroServ);
            var filter=includes.Where(c => c.SeroId.StartsWith(key));
            var res = filter.OrderBy(c=>c.SeroId);
            var result = res.Last();
        }
    }
}
