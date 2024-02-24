using Contract.DTO.CR.Request;
using Contract.DTO.CR.Response;
using Domain.Entities.CR;
using Service.Abstraction.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction.CR
{
    public interface ICustomerClaimService : IServiceEntityBase<CustomerClaimDto>
    {
        Task<CustomerClaimResponseDto> GetClaimById(int cuclCreqEntityId);
        Task<CustomerRequestDto> ClaimPolis(CustomerClaimRequestDto customrClaimDto);
        Task<CustomerRequestDto> ClosePolis(CustomerCloseRequestDto customerCloseDto);
    }
}
