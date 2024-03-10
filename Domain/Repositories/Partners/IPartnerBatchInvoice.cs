using Domain.Entities.Partners;
using Domain.Entities.SO;
using Domain.RequestFeatured;

namespace Domain.Repositories.Partners
{
    public interface IPartnerBatchInvoice
    {
        Task<IEnumerable<BatchPartnerInvoice>> GetAllData();
        Task<IEnumerable<ServiceOrder>> GenerateData();
        Task<int> GetSequence();
        Task CreateBatch(IEnumerable<BatchPartnerInvoice> data);
        Task<PagedList<BatchPartnerInvoice>> GetAllPagingAsync(EntityParameter parameter);
        Task<BatchPartnerInvoice?> GetByid(string id);
    }
}
