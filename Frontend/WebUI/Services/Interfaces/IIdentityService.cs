using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel.Client;
using Shared.Dtos;
using WebUI.Models;

namespace WebUI.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<ResponseDto<bool>> LogIn(LoginInput logIn);
        Task<TokenResponse> GetAccessTokenByRefreshToken();
        Task RevokeRefreshToken();
    }
}
