using Contract.DTO.SO;
using Domain.Repositories.SO;
using Service.Abstraction.SO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction.SO
{
    public interface IServiceRequestSOManager
    {
        IServiceRequestSOBase ServiceRequest { get; }
    }
}
