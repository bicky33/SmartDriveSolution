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
        //public CustomerInscAsset CreateCustomerInscAssets(int entityId,CustomerInscAssetRequestDto customerInscAssetRequestDto,CarSeries carSeries,City existCity,InsuranceType existInty,CustomerRequest newCustomerRequest);
        //public decimal? GetPremiPrice(string insuraceType, string carBrand, int zonesId, decimal currentPrice, int ageOfBirth, List<CustomerInscExtend> cuexs);
        //public void ValidatePoliceNumber(string policeNumber);


    }
}
