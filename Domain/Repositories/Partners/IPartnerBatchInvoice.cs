using Domain.Entities.SO;

namespace Domain.Repositories.Partners
{
    public interface IPartnerBatchInvoice
    {
        Task<IEnumerable<Service>> GetAllData();
    }
}
