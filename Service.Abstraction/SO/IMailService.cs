using Contract.DTO.SO;

namespace Service.Abstraction.SO
{
    public interface IMailService
    {
        void SendEmail(MailData mailData);
    }
}