using Contract.DTO.CR.Request;
using Contract.DTO.CR.Response;
using Domain.Entities.CR;
using Domain.Entities.Master;
using Service.Abstraction.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction.CR
{
    public interface ICustomerInscAssetService : IServiceEntityBase<CustomerInscAssetDto>
    {
        Task<decimal> GetPremiPrice(string insuraceType, int carSeriesId, int cityId, decimal currentPrice);
        Task ValidatePoliceNumber(string policeNumber);


    }
}
