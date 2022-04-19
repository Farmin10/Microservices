using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Shared.Dtos;

namespace Discount.Services
{
    public class DiscountManager : IDiscountServices
    {

        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;
        public DiscountManager(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<ResponseDto<NoContent>> CreateAsync(Models.Discount discount)
        {
            var saveStatus = await _dbConnection.ExecuteAsync("insert into discount(userid,rate,code) values(@UserId,@Rate,@Code)", discount);
            if (saveStatus>0)
            {
                return ResponseDto<NoContent>.Success(204);
            }
            return ResponseDto<NoContent>.Fail("An error occured", 500);
        }

        public async Task<ResponseDto<NoContent>> DeleteAsync(int id)
        {
            var status = await _dbConnection.ExecuteAsync("delete from discount where id=@Id", new { Id = id });
            return status > 0 ? ResponseDto<NoContent>.Success(204) : ResponseDto<NoContent>.Fail("Discount not found", 404);
        }

        public async Task<ResponseDto<List<Models.Discount>>> GetAllAsync()
        {
            var discounts = await _dbConnection.QueryAsync<Models.Discount>("Select*from discount");
            return ResponseDto<List<Models.Discount>>.Success(discounts.ToList(), 200);
        }

        public async Task<ResponseDto<Models.Discount>> GetByCodeAndUserId(string code, string userId)
        {
            var discounts = await _dbConnection.QueryAsync<Models.Discount>("select*from discount where userid=@UserId and code=@Code",new { UserId=userId,Code=code});
            var hasDiscount = discounts.FirstOrDefault();
            if (hasDiscount==null)
            {
                return ResponseDto<Models.Discount>.Fail("Discount Not Found", 404);
            }
            return ResponseDto<Models.Discount>.Success(hasDiscount, 200);
        }

        public async Task<ResponseDto<Models.Discount>> GetByIdAsync(int id)
        {
            var discount = (await _dbConnection.QueryAsync<Models.Discount>("Select*from discount where id=@Id", new { Id = id })).SingleOrDefault();
            if (discount==null)
            {
                return ResponseDto<Models.Discount>.Fail("Discount not fount", 404);
            }
            return ResponseDto<Models.Discount>.Success(discount, 200);
        }

        public async Task<ResponseDto<NoContent>> UpdateAsync(Models.Discount discount)
        {
            var status = await _dbConnection.ExecuteAsync("update discount set userid=@UserId,code=@Code,rate=@Rate where id=@Id", new { Id = discount.Id,UserId=discount.UserId, Code = discount.Code, Rate = discount.Rate });
            if (status>0)
            {
                return ResponseDto<NoContent>.Success(204);
            }
            return ResponseDto<NoContent>.Fail("Discount not Found", 404);
        }
    }
}
