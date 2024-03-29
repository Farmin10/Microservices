﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Basket.Dtos;
using Shared.Dtos;

namespace Basket.Services
{
    public class BasketManager : IBasketService
    {
        private readonly RedisService _redisService;

        public BasketManager(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<ResponseDto<bool>> Delete(string userId)
        {
            var status = await _redisService.GetDb().KeyDeleteAsync(userId);
            return status ? ResponseDto<bool>.Success(204) : ResponseDto<bool>.Fail("Basket Not Found", 404);
        }

        public async Task<ResponseDto<BasketDto>> GetBasket(string userId)
        {
            var existBasket = await _redisService.GetDb().StringGetAsync(userId);
            if (string.IsNullOrEmpty(existBasket))
            {
                return ResponseDto<BasketDto>.Fail("Basket Not Found", 404);
            }

            return ResponseDto<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(existBasket), 200);
        }

        public async Task<ResponseDto<bool>> SaveOrUpdate(BasketDto basketDto)
        {
            var status = await _redisService.GetDb().StringSetAsync(basketDto.UserId, JsonSerializer.Serialize(basketDto));
            return status ? ResponseDto<bool>.Success(204) : ResponseDto<bool>.Fail("Basket could not save or update", 500);
        }
    }
}
