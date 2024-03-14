using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Persistence.Repositories;
using Quartz;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Service.SO.BackgroundService
{
    [DisallowConcurrentExecution]
    public class NotifyCustomerPremiDueDate : IJob
    {
        private readonly ILogger<NotifyCustomerPremiDueDate> _logger;
        private readonly SmartDriveContext _dbContext;
        private readonly IConfiguration _configuration;

        public NotifyCustomerPremiDueDate(ILogger<NotifyCustomerPremiDueDate> logger, SmartDriveContext dbContext, IConfiguration configuration)
        {
            _logger = logger;
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task Execute(IJobExecutionContext context)
        {

            _logger.LogInformation($"Notify Customer Premi payment");
            // get all premi credit 
            var allCredit = await _dbContext.ServicePremiCredits
                .Where(c => c.SecrDuedate.HasValue)
                .Include(c => c.SecrServ)
                    .ThenInclude(c => c.ServCustEntity)
                        .ThenInclude(c => c.UserEntity)
                            .ThenInclude(c => c.User)
                .Select(c => new
                {
                    c.SecrServ.ServCustEntity!.UserEntity.User!.UserEmail,
                    c.SecrDuedate,
                    c.SecrServ.ServCustEntity!.UserEntity.User!.UserFullName,
                }).ToListAsync();


            // Create email message
            List<MailMessage> mailMessages = new();

            // add all recepient
            foreach (var credit in allCredit)
            {
                DateTime dueDate = (DateTime)credit.SecrDuedate!;
                dueDate = dueDate.AddDays(-3);
                if (DateTime.Compare(dueDate, DateTime.Now) == -1)
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(_configuration.GetSection("MailSettings:SenderEmail").Value);
                    mailMessage.To.Add(credit.UserEmail);
                    mailMessage.Subject = "Axa Insurance - Tenggat Waktu Pembayaran Premi";
                    mailMessage.IsBodyHtml = true;
                    StringBuilder mailBody = new StringBuilder();
                    mailBody.AppendFormat($"<h1>Dear, {credit.UserFullName}</h1>");
                    mailBody.AppendFormat("<br />");
                    mailBody.AppendFormat("<p>Waktu pembayaran premi sudah dekat. Tersisa 3 hari untuk melakukan pembayaran premi. Silahkan lakukan pembayaran premi.</p>");
                    mailMessage.Body = mailBody.ToString();

                    mailMessages.Add(mailMessage);
                }
            }


            try
            {
                _logger.LogInformation($"Total item: {mailMessages.Count()}");
                using (var client = new SmtpClient(
                    _configuration.GetSection("MailSettings:Server").Value,
                    Int32.Parse(_configuration.GetSection("MailSettings:Port").Value!)
                    ))
                {
                    // Set up SMTP client
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(_configuration.GetSection("MailSettings:UserName").Value, _configuration.GetSection("MailSettings:Password").Value);

                    // Send email
                    foreach (var message in mailMessages.Select((value, index) => new { value, index }))
                    {
                        await Task.Delay(1 * 1000);
                        client.Send(message.value);
                        _logger.LogInformation($"Customer {allCredit[message.index].UserFullName} successfully notified.");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            finally
            {
                _logger.LogInformation("Task Completed");
            }
        }
    }
}
