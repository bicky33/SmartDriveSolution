using Contract.DTO.Partners;
using Contract.DTO.SO;
using Domain.Entities.Partners;
using Domain.Enum;
using Mapster;
using System.Reflection;

namespace WebApi.Mapping
{
    public class PartnerMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Partner, PartnerDTO>()
                .Map(dest => dest.PartStatus, src => Enum.Parse<PartnerStatus>(src.PartStatus ?? "ACTIVE"))
                .Map(dest => dest.CityName, src => src.PartCity.CityName)
                .Map(dest => dest.PartCityId, src => src.PartCityId);


            config.NewConfig<PartnerAreaWorkgroup, PartnerAreaWorkgroupResponse>()
                .Map(dest => dest.PartName, src => src.Pawo.PacoPatrnEntity.PartName)

                .Map(dest => dest.PawoStatus, src => Enum.Parse<PartnerStatus>(src.PawoStatus ?? "ACTIVE"))

                .Map(dest => dest.ArwgDesc, src => src.PawoArwgCodeNavigation.ArwgDesc)

                .Map(dest => dest.CityName, src => src.PawoArwgCodeNavigation != null
                && src.PawoArwgCodeNavigation.ArwgCity != null
                ? src.PawoArwgCodeNavigation.ArwgCity.CityName : null)

                .Map(dest => dest.ProvName, src => src.PawoArwgCodeNavigation != null
                && src.PawoArwgCodeNavigation.ArwgCity != null
                && src.PawoArwgCodeNavigation.ArwgCity.CityProv != null
                ? src.PawoArwgCodeNavigation.ArwgCity.CityProv.ProvName : null)

                .Map(dest => dest.ZonesName, src => src.PawoArwgCodeNavigation != null
                && src.PawoArwgCodeNavigation.ArwgCity != null
                && src.PawoArwgCodeNavigation.ArwgCity.CityProv != null
                && src.PawoArwgCodeNavigation.ArwgCity.CityProv.ProvZones != null
                ? src.PawoArwgCodeNavigation.ArwgCity.CityProv.ProvZones.ZonesName : null)
                .Map(dest => dest.UserName, src => src.Pawo.PacoUserEntity.UserFullName);

            config.NewConfig<PartnerContact, PartnerContactDTO>()
                .Map(dest => dest.PacoStatus, src => Enum.Parse<PartnerStatus>(src.PacoStatus ?? "ACTIVE"))
                .Map(dest => dest.FullName, src => src.PacoUserEntity != null ? src.PacoUserEntity.UserFullName : null)
                .Map(dest => dest.PhoneNumber, src => src.PacoUserEntity != null
                && src.PacoUserEntity.UserPhones != null ? src.PacoUserEntity.UserPhones.Select(d => d.UsphPhoneNumber).FirstOrDefault() : null)
                .Map(dest => dest.IsGranted, src => src.PacoUserEntity != null &&
                src.PacoUserEntity.UserRoles != null &&
                src.PacoUserEntity.UserRoles.First(x =>
                    x.UsroRoleName.Equals(EnumRoleType.PR)).UsroStatus == EnumRoleActiveStatus.ACTIVE ? true : false
                )
                .Map(dest => dest.PacoPatrnEntityName, src => src.PacoPatrnEntity.PartName);

            config.NewConfig<BatchPartnerInvoice, PartnerBatchInvoiceResponse>()
                .Map(dest => dest.PartnerName, src => src.BpinPatrnEntity != null && src.BpinPatrnEntity.PartName != null ? src.BpinPatrnEntity.PartName : null)
                .Map(dest => dest.PoliceNumber, src => src.BpinSero != null && src.BpinSero.SeroServ != null && src.BpinSero.SeroServ.ServVehicleNo != null
                ? src.BpinSero.SeroServ.ServVehicleNo : null)
                .Map(dest => dest.PolisNumber, src => src.BpinSero != null && src.BpinSero.SeroServ != null && src.BpinSero.SeroServ.ServInsuranceNo != null
                ? src.BpinSero.SeroServ.ServInsuranceNo : null)
                .Map(dest => dest.AccountNumber, src => src.BpinAccountNo)
                .Map(dest => dest.Subtotal, src => src.BpinSubtotal)
                .Map(dest => dest.Tax, src => src.BpinTax)
                .Map(dest => dest.PaidDate, src => src.BpinPaidDate != null ? src.BpinPaidDate : null)
                .Map(dest => dest.InvoiceNo, src => src.BpinInvoiceNo != null ? src.BpinInvoiceNo : null)
                .Map(dest => dest.CreateOn, src => src.BpinCreatedOn != null ? src.BpinCreatedOn : null);

        }
    }
}
