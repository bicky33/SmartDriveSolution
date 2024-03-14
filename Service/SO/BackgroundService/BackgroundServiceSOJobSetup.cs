using Microsoft.Extensions.Options;
using Quartz;

namespace Service.SO.BackgroundService
{
    public class BackgroundServiceSOJobSetup : IConfigureOptions<QuartzOptions>
    {
        public void Configure(QuartzOptions options)
        {
            // add job and trigger
            var jobKey01 = new JobKey(nameof(ChangeAllTaskStatus));
            options
                .AddJob<ChangeAllTaskStatus>(jobBuilder => jobBuilder.WithIdentity(jobKey01))
                .AddTrigger(trigger =>
                {
                    trigger.ForJob(jobKey01).WithCronSchedule("0 5 0 ? * * *");
                });
            var jobKey02 = new JobKey(nameof(ChangeAllServiceStatus));
            options
                .AddJob<ChangeAllServiceStatus>(jobBuilder => jobBuilder.WithIdentity(jobKey02))
                .AddTrigger(trigger =>
                {
                    trigger.ForJob(jobKey02).WithCronSchedule("0 10 0 ? * * *");
                });
            var jobKey03 = new JobKey(nameof(NotifyCustomerPremiDueDate));
            options
                .AddJob<NotifyCustomerPremiDueDate>(jobBuilder => jobBuilder.WithIdentity(jobKey03))
                //.AddTrigger(trigger =>
                //{
                //    trigger.ForJob(jobKey03).StartAt(DateTime.UtcNow.AddHours(1));
                //})
                .AddTrigger(trigger =>
                {
                    trigger.ForJob(jobKey03).WithCronSchedule("0 15 0 ? * * *");
                });
        }
    }
}
