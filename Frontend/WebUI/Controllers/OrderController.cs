﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebUI.Models.OrderVM;
using WebUI.Services.Interfaces;

namespace WebUI.Controllers
{
    public class OrderController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;

        public OrderController(IBasketService basketService, IOrderService orderService)
        {
            _basketService = basketService;
            _orderService = orderService;
        }

        public async Task<IActionResult> Checkout()
        {
            var basket =await _basketService.Get();
            ViewBag.Basket=basket;
            return View(new CheckoutInfoInput());
        }
        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutInfoInput checkoutInfoInput)
        {
            //1.Syncron
            var orderSuspend =await _orderService.SuspendOrder(checkoutInfoInput);
            //var orderStatus = await _orderService.CreateOrder(checkoutInfoInput);
            if (!orderSuspend.IsSuccessful)
            {
                var basket = await _basketService.Get();
                ViewBag.Basket = basket;
                ViewBag.Error = orderSuspend.Error;
                return View();
            }
            //Syncron
            //return RedirectToAction(nameof(SuccessfulCheckout),new {orderId=orderStatus.OrderId });

            //asencron
            return RedirectToAction(nameof(SuccessfulCheckout), new { orderId = new Random().Next(1,1000) });
        }

        public IActionResult SuccessfulCheckout(int orderId)
        { 
            ViewBag.OrderId = orderId;
            return View();
        }

        public async Task<IActionResult> CheckoutHistory()
        { 
            return View(await _orderService.GetOrders());
        }
    }
}
