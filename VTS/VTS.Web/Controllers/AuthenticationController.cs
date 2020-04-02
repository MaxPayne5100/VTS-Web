using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VTS.Web.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using VTS.Core.Settings;

namespace VTS.Web.Controllers
{
    public class AuthenticationController : Controller
    {

        private readonly Services.AuthenticationService.IAuthenticationService _authenticationService;
        private readonly AuthSetting _authSetting;

        public AuthenticationController(Services.AuthenticationService.IAuthenticationService authenticationService,
           IOptions<AuthSetting> authSetting)
        {
            _authenticationService = authenticationService;
            _authSetting = authSetting.Value;
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LogInModel Model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var claimsPrinciple = await _authenticationService.LogIn(
                        Model.Id, Model.Email);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        claimsPrinciple,
                        new AuthenticationProperties
                        {
                            ExpiresUtc = DateTime.UtcNow.AddHours(_authSetting.ExpiredAt)
                        }
                    );

                    return RedirectToAction("Index", "Home");
                }
                catch (ArgumentException e)
                {
                    ViewBag.Error = e.Message;
                    return View(Model);
                }
            }

            return View(Model);
        }
    }
}