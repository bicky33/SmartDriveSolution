using Contract.DTO.SO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction.SO
{
    public interface IServiceRequestSOBase
    { 
        public Task CreateServicePolis(CreateServicePolisDto createServicePolisDto);
        public void Debugging();
    }
}
