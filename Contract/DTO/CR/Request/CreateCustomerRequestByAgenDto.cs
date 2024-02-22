using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.CR.Request
{
    public class CreateCustomerRequestByAgenDto
    {
        public int EmployeeId { get; set; }
        public int CreqAgenEntityid { get; set; }
        public bool IsGranted { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public int CityId { get; set; }
        public string AreaCode { get; set; }
        public string PoliceNumber { get; set; }
        public int CarSeriesId { get; set; }
        public string CarYear { get; set; }
        public string InsuranceType { get; set; }
        public string IsNewChar { get; set; }
        public DateTime PolisStartDate { get; set; }
        public string PaidType { get; set; }
        public decimal CurrentPrice { get; set; }
    }
}
