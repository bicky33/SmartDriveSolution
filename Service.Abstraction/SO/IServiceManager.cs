using Contract.DTO.SO;
using Service.Abstraction.SO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction.SO
{
    public interface IServiceManager
    {
        IServiceEntityBase<ServiceDto,ServiceDtoCreate,int> ServiceService { get; }
        IServiceEntityBase<ServiceOrderDto,ServiceOrderDtoCreate,string> ServiceOrderService { get; }
        IServiceEntityBase<ServiceOrderTaskDto,ServiceOrderTaskDtoCreate,int> ServiceOrderTaskService { get; }
        IServiceEntityBase<ServiceOrderWorkorderDto, ServiceOrderWorkorderDtoCreate, int> ServiceOrderWorkorderService { get; }
    }
}
