using Microsoft.AspNetCore.Http;
using Service.Abstraction.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Service.Helpers
{
    public class FileServer : IFileServer
    {
        public Task Delete(string path)
        {
            if (!File.Exists(path))
            {
                throw new Exception($"file not found with path {path}");
            }

            File.Delete(path);
            return Task.CompletedTask;
        }

        public async Task Save(IFormFile file, string path)
        {
            using var fileStream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(fileStream);
        }
    }
}
