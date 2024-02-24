using Contract.DTO.CR.Request;
using Contract.DTO.CR.Response;
using Domain.RequestFeatured;
using Service.Abstraction.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction.CR
{
    public interface ICustomerRequestService : IServiceEntityBase<CustomerRequestDto>
    {
        Task<IEnumerable<CustomerRequestDto>> GetAllByCustomer(int userId, bool trackChanges);
        Task<IEnumerable<CustomerRequestDto>> GetAllByEmployee(string eawgCode, bool trackChanges);
        Task<CustomerRequestDto> CreateRequest(CreateCustomerRequestDto entity);
        Task<CustomerRequestResponseDto> CreateByEmployee(CreateCustomerRequestByAgenDto entity);
        Task<IEnumerable<CustomerRequestDto>> GetAllPagingAsync(EntityParameter entityParameters, bool trackChanges);
    }
}
