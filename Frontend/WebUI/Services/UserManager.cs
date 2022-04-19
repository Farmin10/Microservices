using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebUI.Models;
using WebUI.Services.Interfaces;

namespace WebUI.Services
{
    public class UserManager : IUserService
    {

        private readonly HttpClient _httpClient;

        public UserManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserViewModel> GetUsers()
        {
            return await _httpClient.GetFromJsonAsync<UserViewModel>("/api/user/getall");
        }
    }
}
