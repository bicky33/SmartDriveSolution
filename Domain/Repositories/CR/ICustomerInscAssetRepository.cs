using Domain.Entities.CR;
using Domain.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.CR
{
    public interface ICustomerInscAssetRepository : IRepositoryEntityBase<CustomerInscAsset>
    {
        CustomerInscAsset CreateData(CustomerInscAsset entity);
        Task<CustomerInscAsset> FindByCiasPoliceNumber(string ciasPoliceNumber, bool trackChanges);
    }
}
