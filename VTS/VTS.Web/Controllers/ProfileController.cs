using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VTS.Web.Controllers
{
    /// <summary>
    /// Controller for Profile.
    /// </summary>
    [Authorize]
    public class ProfileController : Controller
    {
        /// <summary>
        /// Edit get method.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }
    }
}