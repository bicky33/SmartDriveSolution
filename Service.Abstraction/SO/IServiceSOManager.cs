using Contract.DTO.SO;

namespace Service.Abstraction.SO
{
    public interface IServiceSOManager
    {
        IServiceSORelationBase<ServiceDto, ServiceDtoCreate, int> ServiceService { get; }
        IServiceSOEntityBase<ServiceOrderDto, ServiceOrderDtoCreate, string> ServiceOrderService { get; }
        IServiceSOEntityBase<ServiceOrderTaskDto, ServiceOrderTaskDtoCreate, int> ServiceOrderTaskService { get; }
        IServiceSOEntityBase<ServiceOrderWorkorderDto, ServiceOrderWorkorderDtoCreate, int> ServiceOrderWorkorderService { get; }
        IServiceSOEntityBase<ClaimAssetEvidenceDto, ClaimAssetEvidenceDtoCreate, int> ClaimAssetEvidenceService { get; }
        IServiceSOEntityBase<ClaimAssetSparepartDto, ClaimAssetSparepartDtoCreate, int> ClaimAssetSparepartService { get; }
        IServiceSOEntityBase<ServicePremiDto, ServicePremiDtoCreate, int> ServicePremiService { get; }
        IServiceSOEntityBase<ServicePremiCreditDto, ServicePremiCreditDtoCreate, int> ServicePremiCreditService { get; }
    }
}
