using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VTS.Core.Constants;

namespace VTS.Web.Controllers
{
    /// <summary>
    /// Controller for Clerk role.
    /// </summary>
    [Authorize(Policy = Policies.ClerkOnly)]
    public class ClerkController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClerkController"/> class.
        /// </summary>
        public ClerkController()
        {
        }

        /// <summary>
        /// Asynchronous users edit get method.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        public IActionResult UsersEdit()
        {
            return View();
        }
    }
}