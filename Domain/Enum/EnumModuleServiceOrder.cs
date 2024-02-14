using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enum
{
    public class EnumModuleServiceOrder
    {
        public enum SERVTYPE
        {
            FEASIBILITY,
            POLIS,
            CLAIM
        }
        public enum SERVSTATUS
        {
            ACTIVE,
            INACTIVE
        }
        public enum SEROSTATUS
        {
            OPEN,
            CANCELLED,
            REJECT,
            CLOSED
        }
        public enum SEOTSTATUS
        {
            INPROGRESS,
            CANCELLED,
            COMPLETED
        }
        public enum SEROORDTTYPE
        {
            CREATE,
            MODIFY,
            CLOSE
        }
    }
}
