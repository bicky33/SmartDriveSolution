using Domain.Entities.SO;
using Domain.Entities.Users;
using Domain.Enum;
using Domain.Repositories.Partners;
using Domain.RequestFeatured;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Persistence.Repositories.Partners
{
    public class RepositoryPartnerWorkOrder : IRepositoryPartnerWorkOrder
    {
        private readonly SmartDriveContext _context;

        public RepositoryPartnerWorkOrder(SmartDriveContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ServiceOrderWorkorder>> GetAllAsync(int seroPartId, string seotArwgCode)
        {
            return await _context.ServiceOrderWorkorders
                .Where(x =>
                     x.SowoName != null &&
                     x.SowoSeot != null && x.SowoSeot.SeotSero != null && x.SowoSeot.SeotSero.SeroSero != null &&
                     // Service Order Work Order
                     (EF.Functions.Like(x.SowoName, "%repair%") ||
                      EF.Functions.Like(x.SowoName, "%ganti suku%")) &&
                     // Service Order Task Filter
                     x.SowoSeot.SeotName != null &&
                     EF.Functions.Like(x.SowoSeot.SeotName, "%CALCULATE SPARE PART%") &&
                     x.SowoSeot.SeotArwgCode != null && x.SowoSeot.SeotArwgCode.Equals(seotArwgCode) &&
                     x.SowoSeot.SeotStatus != null && x.SowoSeot.SeotStatus.Equals(EnumModuleServiceOrder.SEOTSTATUS.INPROGRESS.ToString())&&
                     // Service Order Filter
                     EF.Functions.Like(x.SowoSeot.SeotSero.SeroId, "CL%") &&
                     x.SowoSeot.SeotSero.SeroPartId.Equals(seroPartId)
                     // Service Filter
                )
                .Select(x => new ServiceOrderWorkorder
                {
                    SowoId = x.SowoId,
                    SowoName = x.SowoName,
                    SowoStatus = x.SowoStatus,
                    SowoSeot = x.SowoSeot != null ? new ServiceOrderTask
                    {
                        SeotId = x.SowoSeot.SeotId,
                        SeotStatus = x.SowoSeot.SeotStatus,
                        SeotName = x.SowoSeot.SeotName,
                        SeotStartdate = x.SowoSeot.SeotStartdate,
                        SeotEnddate = x.SowoSeot.SeotEnddate,
                        SeotSero = x.SowoSeot.SeotSero != null ? new ServiceOrder
                        {
                            SeroPartId = x.SowoSeot.SeotSero.SeroPartId,
                            SeroId = x.SowoSeot.SeotSero.SeroId,
                            ServClaimStartdate = x.SowoSeot.SeotSero.ServClaimStartdate,
                            ServClaimEnddate = x.SowoSeot.SeotSero.ServClaimEnddate,
                            SeroOrdtType = x.SowoSeot.SeotSero.SeroOrdtType,
                            SeroStatus = x.SowoSeot.SeotSero.SeroStatus,
                            SeroServ = x.SowoSeot.SeotSero.SeroServ != null ? new Service
                            {
                                ServServId = x.SowoSeot.SeotSero.SeroServ.ServServId,
                                ServVehicleNo = x.SowoSeot.SeotSero.SeroServ.ServVehicleNo,
                                ServInsuranceNo = x.SowoSeot.SeotSero.SeroServ.ServInsuranceNo,
                                ServType = x.SowoSeot.SeotSero.SeroServ.ServType,
                                ServCustEntity = x.SowoSeot.SeotSero.SeroServ.ServCustEntity != null ? new User
                                {
                                    UserEntityid = x.SowoSeot.SeotSero.SeroServ.ServCustEntity.UserEntityid,
                                    UserFullName = x.SowoSeot.SeotSero.SeroServ.ServCustEntity.UserFullName,
                                } : null,
                            } : null,

                        } : null,

                    } : null,
                }).ToListAsync();
        }

        public async Task<PagedList<ServiceOrderWorkorder>> GetAllAsyncPaging(int seroPartId, string seotArwgCode, EntityParameter parameter)
        {
            IQueryable<ServiceOrderWorkorder> result = _context.ServiceOrderWorkorders
                .AsNoTracking()
                .Where(x =>
                    x.SowoStatus.Equals(EnumModuleServiceOrder.SEOTSTATUS.INPROGRESS) &&
                     x.SowoName != null &&
                     x.SowoSeot != null && x.SowoSeot.SeotSero != null && x.SowoSeot.SeotSero.SeroSero != null &&
                     // Service Order Work Order
                     (EF.Functions.Like(x.SowoName, "%repair%") ||
                      EF.Functions.Like(x.SowoName, "%ganti suku%")) &&
                     // Service Order Task Filter
                     x.SowoSeot.SeotName != null &&
                     EF.Functions.Like(x.SowoSeot.SeotName, "%CALCULATE SPARE PART%") &&
                     x.SowoSeot.SeotArwgCode != null && x.SowoSeot.SeotArwgCode.Equals(seotArwgCode) &&
                     x.SowoSeot.SeotStatus != null && x.SowoSeot.SeotStatus.Equals(EnumModuleServiceOrder.SEOTSTATUS.INPROGRESS.ToString()) &&
                     // Service Order Filter
                     EF.Functions.Like(x.SowoSeot.SeotSero.SeroId, "CL%") &&
                     x.SowoSeot.SeotSero.SeroPartId.Equals(seroPartId)
                // Service Filter
                )
                .Select(x => new ServiceOrderWorkorder
                {
                    SowoId = x.SowoId,
                    SowoName = x.SowoName,
                    SowoStatus = x.SowoStatus,
                    SowoSeot = x.SowoSeot != null ? new ServiceOrderTask
                    {
                        SeotId = x.SowoSeot.SeotId,
                        SeotStatus = x.SowoSeot.SeotStatus,
                        SeotName = x.SowoSeot.SeotName,
                        SeotStartdate = x.SowoSeot.SeotStartdate,
                        SeotEnddate = x.SowoSeot.SeotEnddate,
                        SeotSero = x.SowoSeot.SeotSero != null ? new ServiceOrder
                        {
                            SeroPartId = x.SowoSeot.SeotSero.SeroPartId,
                            SeroId = x.SowoSeot.SeotSero.SeroId,
                            ServClaimStartdate = x.SowoSeot.SeotSero.ServClaimStartdate,
                            ServClaimEnddate = x.SowoSeot.SeotSero.ServClaimEnddate,
                            SeroOrdtType = x.SowoSeot.SeotSero.SeroOrdtType,
                            SeroStatus = x.SowoSeot.SeotSero.SeroStatus,
                            SeroServ = x.SowoSeot.SeotSero.SeroServ != null ? new Service
                            {
                                ServServId = x.SowoSeot.SeotSero.SeroServ.ServServId,
                                ServVehicleNo = x.SowoSeot.SeotSero.SeroServ.ServVehicleNo,
                                ServInsuranceNo = x.SowoSeot.SeotSero.SeroServ.ServInsuranceNo,
                                ServType = x.SowoSeot.SeotSero.SeroServ.ServType,
                                ServCustEntity = x.SowoSeot.SeotSero.SeroServ.ServCustEntity != null ? new User
                                {
                                    UserEntityid = x.SowoSeot.SeotSero.SeroServ.ServCustEntity.UserEntityid,
                                    UserFullName = x.SowoSeot.SeotSero.SeroServ.ServCustEntity.UserFullName,
                                } : null,
                            } : null,

                        } : null,

                    } : null,
                });

            return PagedList<ServiceOrderWorkorder>.ToPagedList(result, parameter.PageNumber, parameter.PageSize);
        }
    }
}
