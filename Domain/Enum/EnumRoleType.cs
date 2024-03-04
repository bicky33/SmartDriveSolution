using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enum
{
    public static class EnumRoleType
    {
        public static readonly string EM = "EM";
        public static readonly string CU = "CU";
        public static readonly string PC = "PC";
        public static readonly string PR = "PR";
        public static readonly string AD = "AD";
    }

    public static class EnumRoleActiveStatus
    {
        public static readonly string ACTIVE = "ACTIVE";
        public static readonly string INACTIVE = "INACTIVE";
    }
}
