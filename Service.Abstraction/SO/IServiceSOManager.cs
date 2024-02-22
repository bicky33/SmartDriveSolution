using Contract.DTO.SO;
using Service.Abstraction.SO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction.SO
{
    public interface IServiceSOManager
    {
        IServiceSORelationBase<ServiceDto,ServiceDtoCreate,int> ServiceService { get; }
        IServiceSOEntityBase<ServiceOrderDto,ServiceOrderDtoCreate,string> ServiceOrderService { get; }
        IServiceSOEntityBase<ServiceOrderTaskDto,ServiceOrderTaskDtoCreate,int> ServiceOrderTaskService { get; }
        IServiceSOEntityBase<ServiceOrderWorkorderDto, ServiceOrderWorkorderDtoCreate, int> ServiceOrderWorkorderService { get; }
        IServiceSOEntityBase<ClaimAssetEvidenceDto, ClaimAssetEvidenceDtoCreate, int> ClaimAssetEvidenceService { get; }
        IServiceSOEntityBase<ClaimAssetSparepartDto, ClaimAssetSparepartDtoCreate, int> ClaimAssetSparepartService { get; }
        IServiceSOEntityBase<ServicePremiDto, ServicePremiDtoCreate, int> ServicePremiService { get; }
        IServiceSOEntityBase<ServicePremiCreditDto, ServicePremiCreditDtoCreate, int> ServicePremiCreditService { get; }
    }
}
