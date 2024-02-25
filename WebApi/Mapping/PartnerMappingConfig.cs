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
                .Map(dest => dest.PartStatus, src => Enum.Parse<PartnerStatus>(src.PartStatus ?? "ACTIVE"));


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
                ? src.PawoArwgCodeNavigation.ArwgCity.CityProv.ProvZones.ZonesName : null);

            config.NewConfig<PartnerContact, PartnerContactDTO>()
                .Map(dest => dest.PacoStatus, src => Enum.Parse<PartnerStatus>(src.PacoStatus ?? "ACTIVE"))
                .Map(dest => dest.FullName, src => src.PacoUserEntity != null ? src.PacoUserEntity.UserFullName : null )
                .Map(dest => dest.PhoneNumber, src => src.PacoUserEntity != null 
                && src.PacoUserEntity.UserPhones != null ? src.PacoUserEntity.UserPhones.Select(d => d.UsphPhoneNumber).FirstOrDefault() : null);

        }
    }
}
