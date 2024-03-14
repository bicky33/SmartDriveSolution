using Contract.DTO.SO;
using Microsoft.Extensions.Configuration;
using Service.Abstraction.SO;
using System.Net;
using System.Net.Mail;

namespace Service.SO
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail(MailData mailData)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(_configuration.GetSection("MailSettings:SenderEmail").Value);
                mailMessage.To.Add(mailData.EmailAdress);
                mailMessage.Subject = mailData.EmailSubject;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = mailData.EmailBody;


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
                    client.Send(mailMessage);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
