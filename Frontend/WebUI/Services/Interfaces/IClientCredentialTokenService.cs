using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Services.Interfaces
{ 
    public interface IClientCredentialTokenService
    {
        Task<string> GetToken();
    }
}
