using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Records
{
    public record PaginationDTO<T>(
        int TotalPages,
        int CurrentPages,
        List<T> Data
    ) where T: class;
}
