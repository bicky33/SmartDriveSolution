using Contract.DTO.Partners;
using Domain.Entities.Partners;
using Domain.Entities.SO;
using Domain.Enum;
using Domain.Repositories.Partners;
using Domain.RequestFeatured;
using Mapster;
using Service.Abstraction.Partners;

namespace Service.Partners
{
    public class ServicePartnerBatchInvoice : IServicePartnerBatchInvoice
    {
        private readonly IRepositoryPartnerManager _repositoryPartnerManager;

        public ServicePartnerBatchInvoice(IRepositoryPartnerManager repositoryPartnerManager)
        {
            _repositoryPartnerManager = repositoryPartnerManager;
        }

        public async Task CreateBatch()
        {
            IEnumerable<ServiceOrder> data = await _repositoryPartnerManager.RepositoryPartnerBatchInvoice.GenerateData();
            IEnumerable<BatchPartnerInvoice> result = await PrepareData(data);
            await _repositoryPartnerManager.RepositoryPartnerBatchInvoice.CreateBatch(result);
            await _repositoryPartnerManager.UnitOfWorks.SaveChangesAsync();
        }

        private async Task<IEnumerable<BatchPartnerInvoice>> PrepareData(IEnumerable<ServiceOrder> data)
        {
            List<Task<BatchPartnerInvoice>> tasks = new List<Task<BatchPartnerInvoice>>();

            foreach (var xx in data)
            {
                var invoice = new BatchPartnerInvoice
                {
                    BpinInvoiceNo = await GenerateInvoice(),
                    BpinSeroId = xx.SeroId,
                    BpinCreatedOn = DateTime.Now,
                    BpinStatus = BpinStatus.NOT_PAID.ToString(),
                    BpinSubtotal = (xx.ClaimAssetSpareparts.Sum(xxx => xxx.CaspSubtotal) + xx.ClaimAssetEvidences.Sum(xxx => xxx.CaevServiceFee)),
                    BpinTax = 0.5m * (xx.ClaimAssetSpareparts.Sum(xxx => xxx.CaspSubtotal) + xx.ClaimAssetEvidences.Sum(xxx => xxx.CaevServiceFee)),
                    BpinPatrnEntityid = xx.SeroPart?.PartEntityid,
                    BpinAccountNo = xx.SeroPart?.PartAccountNo,
                };

                tasks.Add(Task.FromResult(invoice));
            }

            return await Task.WhenAll(tasks);

        }
        private async Task<string> GenerateInvoice()
        {
            int sequence = await _repositoryPartnerManager.RepositoryPartnerBatchInvoice.GetSequence();
            string invoice = $"INVPTR{DateTime.Now:yyyyMMdd}{sequence}";
            return invoice;
        }


        public async Task<IEnumerable<PartnerBatchInvoiceResponse>> GetAll()
        {
            IEnumerable<BatchPartnerInvoice> data =  await _repositoryPartnerManager.RepositoryPartnerBatchInvoice.GetAllData();
            return data.Adapt<IEnumerable<PartnerBatchInvoiceResponse>>();
        }

        public async Task<IEnumerable<PartnerBatchInvoiceResponse>> GetAllPagingAsync(EntityParameter parameter)
        {
            PagedList<BatchPartnerInvoice> invoices = await _repositoryPartnerManager.RepositoryPartnerBatchInvoice.GetAllPagingAsync(parameter);
            return invoices.Adapt<IEnumerable<PartnerBatchInvoiceResponse>>();
        }
    }
}
