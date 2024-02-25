using Domain.Entities.SO;
using Domain.Enum;
using Domain.Repositories.Partners;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories.Partners
{
    internal class RepositoryPartnerBatchInvoice : IPartnerBatchInvoice
    {
        private readonly SmartDriveContext _context;

        public RepositoryPartnerBatchInvoice(SmartDriveContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Service>> GetAllData()
        {
            IQueryable<Service> data =  _context.Services.Where(x =>
                x.ServType != null &&  x.ServType.Equals(EnumModuleServiceOrder.SERVTYPE.CLAIM.ToString()) &&
                x.ServiceOrders.All(xx => xx.SeroStatus != null && xx.SeroStatus.Equals(EnumModuleServiceOrder.SEROSTATUS.CLOSED) && xx.SeroPartId != null)
            ).Select( xxx => new Service
            {
                ServInsuranceNo = xxx.ServInsuranceNo,
                ServVehicleNo = xxx.ServVehicleNo,
                ServiceOrders = xxx.ServiceOrders.Select(x => 
                    new ServiceOrder { 
                        SeroId = x.SeroId,
                        SeroStatus = x.SeroStatus,
                        ClaimAssetEvidences = x.ClaimAssetEvidences.Select(xx => new ClaimAssetEvidence
                        {
                            CaevServiceFee = xx.CaevServiceFee
                        }).ToList(),
                        ClaimAssetSpareparts = x.ClaimAssetSpareparts.Select(xx => new ClaimAssetSparepart
                        {
                            CaspSubtotal = xx.CaspSubtotal
                        }).ToList()
                    }).ToList()
            });
            return await data.ToListAsync();
        }
    }
}
