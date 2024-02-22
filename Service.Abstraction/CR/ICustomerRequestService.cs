﻿using Contract.DTO.CR.Request;
using Contract.DTO.CR.Response;
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
        Task<IEnumerable<CustomerRequestDto>> GetAllByUser(int userId, bool trackChanges);
        Task<IEnumerable<CustomerRequestDto>> GetAllByEmployee(string eawgCode, bool trackChanges);
        Task<CustomerRequestDto> CreateRequest(CreateCustomerRequestDto entity);
        Task<CustomerRequestResponseDto> CreateByUser(CustomerRequestRequestDto entity);
        Task<CustomerRequestResponseDto> CreateByEmployee(CreateCustomerRequestByAgenDto entity);
    }
}
