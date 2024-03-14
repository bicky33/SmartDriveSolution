using Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Repositories;
using Quartz;
using System.Data;

namespace Service.SO.BackgroundService
{
    [DisallowConcurrentExecution]
    public class ChangeAllTaskStatus : IJob
    {
        private readonly ILogger<ChangeAllTaskStatus> _logger;
        private readonly SmartDriveContext _dbContext;

        public ChangeAllTaskStatus(ILogger<ChangeAllTaskStatus> logger, SmartDriveContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Start Task : Check all service status End Date");
            try
            {
                var allTask = await _dbContext.ServiceOrderTasks.Where(c => c.SeotStatus!.Equals(EnumModuleServiceOrder.SEOTSTATUS.INPROGRESS.ToString()) && c.SeotEnddate != null)
                    .ToListAsync();
                _logger.LogInformation($"Total Item in checking : {allTask.Count()}");
                foreach (var task in allTask)
                {
                    var result = DateTime.Compare((DateTime)task.SeotEnddate!, DateTime.Now);
                    if (result == -1)
                    {
                        task.SeotStatus = EnumModuleServiceOrder.SEOTSTATUS.CANCELLED.ToString();
                        _dbContext.Update(task);
                        _logger.LogInformation($"Task with ID : {task.SeotId} has exceed due date and has cancelled");
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
