using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Dtos;

namespace Discount.Services
{
    public interface IDiscountServices
    {
        Task<ResponseDto<List<Models.Discount>>> GetAllAsync();
        Task<ResponseDto<Models.Discount>> GetByIdAsync(int id);
        Task<ResponseDto<NoContent>> DeleteAsync(int id);
        Task<ResponseDto<NoContent>> CreateAsync(Models.Discount discount);
        Task<ResponseDto<NoContent>> UpdateAsync(Models.Discount discount);

        Task<ResponseDto<Models.Discount>> GetByCodeAndUserId(string code,string userId);
    }
}
