using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Basket.Dtos;
using Shared.Dtos;

namespace Basket.Services
{
    public interface IBasketService
    {
        Task<ResponseDto<BasketDto>> GetBasket(string userId);
        Task<ResponseDto<bool>> SaveOrUpdate(BasketDto basketDto);
        Task<ResponseDto<bool>> Delete(string userId);
    }
}
