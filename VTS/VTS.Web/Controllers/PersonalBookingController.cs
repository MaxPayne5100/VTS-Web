using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VTS.Web.Controllers
{
    /// <summary>
    /// Controller for personal bookings.
    /// </summary>
    [Authorize]
    public class PersonalBookingController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonalBookingController"/> class.
        /// </summary>
        public PersonalBookingController()
        {
        }

        /// <summary>
        /// Personal bookings edit get method.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }
    }
}