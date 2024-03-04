using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.UserModule
{
    public class LoginResponseDto
    {
        public string AccessToken {  get; set; }
        public LoginClaimsDto UserData { get; set; }
    }
}
