using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VTS.Core.Constants;

namespace VTS.Web.Controllers
{
    /// <summary>
    /// Controller for Manager role.
    /// </summary>
    [Authorize(Policy = Policies.ManagerOnly)]
    public class ManagerController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerController"/> class.
        /// </summary>
        public ManagerController()
        {
        }

        /// <summary>
        /// Users edit get method.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        public IActionResult UsersEdit()
        {
            return View();
        }
    }
}