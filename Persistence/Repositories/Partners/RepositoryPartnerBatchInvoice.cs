using Domain.Entities.Partners;
using Domain.Entities.SO;
using Domain.Enum;
using Domain.Repositories.Partners;
using Domain.RequestFeatured;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Persistence.Repositories.Partners
{
    internal class RepositoryPartnerBatchInvoice : IPartnerBatchInvoice
    {
        private readonly SmartDriveContext _context;

        public RepositoryPartnerBatchInvoice(SmartDriveContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ServiceOrder>> GenerateData()
        {
            DateTime now = DateTime.Now;
            DateTime startDate = new (now.Year, (now.Month -1), 25);
            DateTime endDate = new(now.Year, now.Month, 25);
            IQueryable<ServiceOrder> data =  _context.ServiceOrders.AsNoTracking().Where(xx =>
                xx.SeroStatus != null && xx.SeroStatus.Equals(EnumModuleServiceOrder.SEROSTATUS.CLOSED.ToString()) && xx.SeroPartId != null &&
                xx.SeroServ != null && xx.SeroServ.ServType != null && xx.SeroServ.ServType.Equals(EnumModuleServiceOrder.SERVTYPE.CLAIM.ToString()) &&
                xx.SeroServ.ServEnddate != null && startDate <= xx.SeroServ.ServEnddate && endDate >= xx.SeroServ.ServEnddate
            ).Select( xxx => 
                new ServiceOrder { 
                    SeroId = xxx.SeroId,
                    SeroStatus = xxx.SeroStatus,
                    ClaimAssetEvidences = xxx.ClaimAssetEvidences.Select(xx => new ClaimAssetEvidence
                    {
                        CaevServiceFee = xx.CaevServiceFee
                    }).ToList(),
                    ClaimAssetSpareparts = xxx.ClaimAssetSpareparts.Select(xx => new ClaimAssetSparepart
                    {
                        CaspSubtotal = xx.CaspSubtotal
                    }).ToList(),
                    SeroPart = xxx.SeroPart != null ? new Partner
                    {
                        PartEntityid = xxx.SeroPart.PartEntityid,
                        PartAccountNo = xxx.SeroPart.PartAccountNo,
                        PartName = xxx.SeroPart.PartName,
                    } : null,
                    SeroServ = xxx.SeroServ != null ? new Domain.Entities.SO.Service
                    {
                        ServInsuranceNo = xxx.SeroServ.ServInsuranceNo,
                        ServVehicleNo = xxx.SeroServ.ServVehicleNo
                    } : null
                }
            );
            return await data.ToListAsync();
        }

        public async Task<int> GetSequence()
        {
            SqlParameter parameter = new SqlParameter("@result", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output,
            };

            string sql = "SET @result = (NEXT VALUE FOR partners.PartnerBatchInvoiceSequence)";
            await _context.Database.ExecuteSqlRawAsync(sql, parameter);
            int sequence = (int) parameter.Value;
            return sequence;

        }

        public async Task CreateBatch(IEnumerable<BatchPartnerInvoice> data)
        {
            await _context.BatchPartnerInvoices.AddRangeAsync(data);
        }

        public async Task<IEnumerable<BatchPartnerInvoice>> GetAllData()
        {
            return await _context.BatchPartnerInvoices.AsNoTracking()
                .Where(x => x.BpinStatus != null && x.BpinStatus.Equals(BpinStatus.NOT_PAID.ToString()))
                .Select(xx => new BatchPartnerInvoice
                {
                    BpinAccountNo = xx.BpinAccountNo,
                    BpinStatus = xx.BpinStatus,
                    BpinInvoiceNo = xx.BpinInvoiceNo,
                    BpinCreatedOn = xx.BpinCreatedOn,
                    BpinPaidDate = xx.BpinPaidDate,
                    BpinSubtotal = xx.BpinSubtotal,
                    BpinTax = xx.BpinTax,
                    BpinPatrnEntity = xx.BpinPatrnEntity != null ? new Partner
                    {
                        PartEntityid = xx.BpinPatrnEntity.PartEntityid,
                        PartName = xx.BpinPatrnEntity.PartName,
                    } : null,
                    BpinSero =  new ServiceOrder
                    {
                        SeroId = xx.BpinSero.SeroId,
                        SeroServ = xx.BpinSero.SeroServ != null ? new Domain.Entities.SO.Service
                        {
                            ServInsuranceNo = xx.BpinSero.SeroServ.ServInsuranceNo,
                            ServVehicleNo = xx.BpinSero.SeroServ.ServVehicleNo
                        } : null
                    }
                }).ToListAsync();
        }

        public async Task<PagedList<BatchPartnerInvoice>> GetAllPagingAsync(EntityParameter parameter)
        {
            IQueryable<BatchPartnerInvoice> invoices = _context.BatchPartnerInvoices.AsNoTracking()
                .Where(x => x.BpinStatus != null && x.BpinStatus.Equals(BpinStatus.NOT_PAID.ToString()))
                .Select(xx => new BatchPartnerInvoice
                {
                    BpinAccountNo = xx.BpinAccountNo,
                    BpinStatus = xx.BpinStatus,
                    BpinInvoiceNo = xx.BpinInvoiceNo,
                    BpinCreatedOn = xx.BpinCreatedOn,
                    BpinPaidDate = xx.BpinPaidDate,
                    BpinSubtotal = xx.BpinSubtotal,
                    BpinTax = xx.BpinTax,
                    BpinPatrnEntity = xx.BpinPatrnEntity != null ? new Partner
                    {
                        PartEntityid = xx.BpinPatrnEntity.PartEntityid,
                        PartName = xx.BpinPatrnEntity.PartName,
                    } : null,
                    BpinSero = new ServiceOrder
                    {
                        SeroId = xx.BpinSero.SeroId,
                        SeroServ = xx.BpinSero.SeroServ != null ? new Domain.Entities.SO.Service
                        {
                            ServInsuranceNo = xx.BpinSero.SeroServ.ServInsuranceNo,
                            ServVehicleNo = xx.BpinSero.SeroServ.ServVehicleNo
                        } : null
                    }
                });
            return PagedList<BatchPartnerInvoice>.ToPagedList(invoices, parameter.PageNumber, parameter.PageSize);
        }
    }
}
