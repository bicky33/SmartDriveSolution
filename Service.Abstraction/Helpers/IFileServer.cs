using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Service.Abstraction.Helpers
{
    public interface IFileServer
    {
        Task Save(IFormFile file, string path);
        Task Delete(string path);
    }
}
