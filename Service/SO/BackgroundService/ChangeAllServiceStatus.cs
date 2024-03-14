using Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Repositories;
using Quartz;

namespace Service.SO.BackgroundService
{
    [DisallowConcurrentExecution]
    public class ChangeAllServiceStatus : IJob
    {
        private readonly ILogger<ChangeAllTaskStatus> _logger;
        private readonly SmartDriveContext _dbContext;

        public ChangeAllServiceStatus(ILogger<ChangeAllTaskStatus> logger, SmartDriveContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            // get all data service 

            _logger.LogInformation("Start Task : Check all service status End Date");
            try
            {
                var allServ = await _dbContext.Services.Where(c => c.ServEnddate != null && c.ServStatus != EnumModuleServiceOrder.SERVSTATUS.INACTIVE.ToString()).ToListAsync();
                _logger.LogInformation($"Total Item in checking : {allServ.Count()}");
                foreach (var serv in allServ)
                {
                    var result = DateTime.Compare((DateTime)serv.ServEnddate!, DateTime.Now);
                    if (result == -1)
                    {
                        serv.ServStatus = EnumModuleServiceOrder.SERVSTATUS.INACTIVE.ToString();
                        _dbContext.Update(serv);
                        _logger.LogInformation($"Service with ID : {serv.ServId} has exceed due date and has change to INACTIVE");
                    }
                }
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            finally
            {
                _logger.LogInformation("Task Completed");
            }
        }
    }
}
