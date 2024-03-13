using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RequestFeatured.SO
{
    public class EntityParameterSO : EntityParameter
    {
        public string? Value { get; set; }
        public string? SearchType { get; set; }
        public string? SearchStatus { get; set; }
        public string? SearchStartDate { get; set; }
        public string? SearchEndDate { get; set; }
    }
}
