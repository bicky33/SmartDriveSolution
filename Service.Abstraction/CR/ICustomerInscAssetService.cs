using Contract.DTO.CR.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction.CR
{
    public interface ICustomerInscAssetService
    {
        Task<IEnumerable<CustomerInscAssetDto>> GetAllAsync(bool trackChanges);
        Task<CustomerInscAssetDto> GetByIdAsync(int id, bool trackChanges);
        Task<CustomerInscAssetDto> CreateAsync(CustomerInscAssetDto entity);
        Task DeleteAsync(int id);
    }
}
