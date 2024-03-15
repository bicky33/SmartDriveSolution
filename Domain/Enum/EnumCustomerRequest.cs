using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enum
{
    public class EnumCustomerRequest
    {
        public enum CREQSTATUS
        {
            OPEN,
            CLOSED,
            CANCELED,
            REJECT
        }
        public enum CREQTYPE
        {
            POLIS,
            CLAIM,
            CLOSE,
            FEASIBILITY
        }
        public enum CADOCCATEGORY
        {
            KTP,
            SIUP,
            TDP
        }
        public enum CREQPAIDTYPE
        {
            CASH,
            CREDIT
        }
    }
}
