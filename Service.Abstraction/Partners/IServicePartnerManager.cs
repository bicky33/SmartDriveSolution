using Domain.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction.Partners
{
    public interface IServicePartnerManager
    {
        public IServicePartner ServicePartner { get; }
        public IServicePartnerAreaWorkgroup ServicePartnerAreaWorkgroup { get; }
        public IServicePartnerContact ServicePartnerContact { get; }
    }
}
