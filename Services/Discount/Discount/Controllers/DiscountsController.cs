using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discount.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.ControllerBases;
using Shared.Services;

namespace Discount.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : CustomBaseController
    {
        private readonly IDiscountServices _discountServices;
        private readonly ISharedIdentityService _sharedIdentityService;
        public DiscountsController(IDiscountServices discountServices, ISharedIdentityService sharedIdentityService)
        {
            _discountServices = discountServices;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionTResultInstance(await _discountServices.GetAllAsync());
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            var discount = await _discountServices.GetByIdAsync(id);
            return CreateActionTResultInstance(discount);
        }

        [HttpGet]
        [Route("/api/[controller]/[action]/{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            var userId = _sharedIdentityService.GetUserId;
            var discount = await _discountServices.GetByCodeAndUserId(code, userId);
            return CreateActionTResultInstance(discount);
        }


        [HttpPost]
        public async Task<IActionResult> Create(Models.Discount discount)
        {
            return CreateActionTResultInstance(await _discountServices.CreateAsync(discount));
        }


        [HttpPut]
        public async Task<IActionResult> Update(Models.Discount discount)
        {
            return CreateActionTResultInstance(await _discountServices.UpdateAsync(discount));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return CreateActionTResultInstance(await _discountServices.DeleteAsync(id));
        }

    }
}
