using Contract.DTO.Partners;
using Domain.Entities.SO;
using Mapster;

namespace WebApi.Mapping
{
    public class ServiceMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ServiceOrderWorkorder, PartnerWorkOrderResponse>()
                .Map(dest => dest.CustomerName, src => src.SowoSeot != null
                && src.SowoSeot.SeotSero != null
                && src.SowoSeot.SeotSero.SeroServ != null
                && src.SowoSeot.SeotSero.SeroServ.ServCustEntity != null ?
                src.SowoSeot.SeotSero.SeroServ.ServCustEntity : null)
                .Map(dest => dest.PoliceNumber, src => src.SowoSeot != null
                && src.SowoSeot.SeotSero != null
                && src.SowoSeot.SeotSero.SeroServ != null
                && src.SowoSeot.SeotSero.SeroServ.ServVehicleNo != null ? src.SowoSeot.SeotSero.SeroServ.ServVehicleNo : null)
                .Map(dest => dest.ServInsuranceNo, src => src.SowoSeot != null
                && src.SowoSeot.SeotSero != null
                && src.SowoSeot.SeotSero.SeroServ != null
                && src.SowoSeot.SeotSero.SeroServ.ServInsuranceNo != null ? src.SowoSeot.SeotSero.SeroServ.ServInsuranceNo : null)
                .Map(dest => dest.ServiceType, src => src.SowoSeot != null
                && src.SowoSeot.SeotSero != null
                && src.SowoSeot.SeotSero.SeroServ != null
                && src.SowoSeot.SeotSero.SeroServ.ServType != null ? src.SowoSeot.SeotSero.SeroServ.ServType : null)
                .Map(dest => dest.StartDate, src => src.SowoSeot != null
                ? src.SowoSeot.SeotStartdate : null)
                .Map(dest => dest.EndDate, src => src.SowoSeot != null
                ? src.SowoSeot.SeotEnddate : null)
                .Map(dest => dest.Status, src => src.SowoSeot != null
                && src.SowoSeot.SeotSero != null
                ? src.SowoSeot.SeotSero.SeroStatus : null)
                .Map(dest => dest.SeroPartId, src => src.SowoSeot != null
                && src.SowoSeot.SeotSero != null
                && src.SowoSeot.SeotSero != null
                ? src.SowoSeot.SeotSero.SeroPartId: null)
                .Map(dest => dest.Status, src => src.SowoSeot != null
                && src.SowoSeot.SeotSero != null
                ? src.SowoSeot.SeotSero.SeroStatus : null)
                .Map(dest => dest.WorkOrder, src => src.SowoName);
        }
    }
}
