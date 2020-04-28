using System;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VTS.Core.Constants;

namespace VTS.Web.Controllers
{
    /// <summary>
    /// Controller for personal bookings.
    /// </summary>
    [Authorize]
    public class PersonalBookingController : Controller
    {
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonalBookingController"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        public PersonalBookingController(IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
        }

        /// <summary>
        /// Personal bookings edit get method.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        public IActionResult Edit()
        {
            var id = uint.Parse(User.FindFirst(ClaimKeys.Id).Value);
            return View(id);
        }
    }
}