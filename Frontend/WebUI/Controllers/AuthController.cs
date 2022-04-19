using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;
using WebUI.Services.Interfaces;

namespace WebUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IIdentityService _ıdentityService;

        public AuthController(IIdentityService ıdentityService)
        {
            _ıdentityService = ıdentityService;
        }

        public IActionResult Login()
        {

            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Login(LoginInput loginInput)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var response = await _ıdentityService.LogIn(loginInput);
            if (!response.IsSuccess)
            {
                response.Errors.ForEach(x =>
                {
                    ModelState.AddModelError(string.Empty, x);
                });

                return View();
            }

            return RedirectToAction(nameof(Index), "Home");
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await _ıdentityService.RevokeRefreshToken();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
