using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Dtos
{
    public class UserForRegisterDto
    {
        public string  UserName { get; set; }
        public string  City { get; set; }
        public string  Email { get; set; }
        public string  Password { get; set; }
    }
}
