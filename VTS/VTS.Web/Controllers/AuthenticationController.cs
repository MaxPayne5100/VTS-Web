using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using VTS.Core.Settings;
using VTS.Web.Models;

namespace VTS.Web.Controllers
{
    /// <summary>
    /// Controller for Authentication.
    /// </summary>
    public class AuthenticationController : Controller
    {
        private readonly Services.AuthenticationService.IAuthenticationService _authenticationService;
        private readonly AuthSetting _authSetting;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
        /// </summary>
        /// <param name="authenticationService">Service for Authentication.</param>
        /// <param name="authSetting">Authentication settings.</param>
        public AuthenticationController(
            Services.AuthenticationService.IAuthenticationService authenticationService,
            IOptions<AuthSetting> authSetting)
        {
            _authenticationService = authenticationService;
            _authSetting = authSetting.Value;
        }

        /// <summary>
        /// Login get method.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        /// <summary>
        /// Asynchronous logout get method.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("LogIn", "Authentication");
        }

        /// <summary>
        /// Asynchronous login post method.
        /// </summary>
        /// <param name="model">Login model.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        public async Task<IActionResult> LogIn(LogInModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var claimsPrinciple = await _authenticationService.LogIn(
                        model.Id, model.Email);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        claimsPrinciple,
                        new AuthenticationProperties
                        {
                            ExpiresUtc = DateTime.UtcNow.AddHours(_authSetting.ExpiredAt),
                        });

                    return RedirectToAction("Index", "Home");
                }
                catch (ArgumentException e)
                {
                    ViewBag.Error = e.Message;
                    return View(model);
                }
            }

            return View(model);
        }

        /// <summary>
        /// Get method to restrict access.
        /// </summary>
        /// <param name="returnUrl">URL.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        public IActionResult AccessDenied(string returnUrl)
        {
            string message;

            if (returnUrl.StartsWith("/Clerk"))
            {
                message = $"Only clerks can access {returnUrl}";
            }
            else if (returnUrl.StartsWith("/Manager"))
            {
                message = $"Only managers  can access {returnUrl}";
            }
            else
            {
                message = "";
            }

            return View("AccessDenied", message);
        }
    }
}