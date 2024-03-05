using Domain.Enum;

namespace Contract.DTO.Partners
{
    public record PartnerAreaWorkgroupResponse(
        int PawoPatrEntityid,
        string PawoArwgCode,
        int PawoUserEntityid,
        PartnerStatus PawoStatus,
        DateTime? PawoModifiedDate,
        string PartName,
        string ArwgDesc,
        string CityName,
        string ProvName,
        string ZonesName,
        string UserName
    );
}
