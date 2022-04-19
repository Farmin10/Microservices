using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models;

namespace WebUI.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> GetUsers(); 
    }
}
