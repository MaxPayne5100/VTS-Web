using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VTS.Core.Constants;
using VTS.Services.BookingService;
using VTS.Services.UserVacationInfoService;

namespace VTS.Web.Controllers
{
    /// <summary>
    /// Controller for personal bookings.
    /// </summary>
    [Authorize]
    public class PersonalBookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IUserVacationInfoService _userVacationInfoService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonalBookingController"/> class.
        /// </summary>
        /// <param name="bookingService">Booking service.</param>
        /// <param name="userVacationInfoService">UserVacationInfo service.</param>
        public PersonalBookingController(IBookingService bookingService, IUserVacationInfoService userVacationInfoService)
        {
            _bookingService = bookingService ?? throw new ArgumentException(nameof(bookingService));
            _userVacationInfoService = userVacationInfoService ?? throw new ArgumentException(nameof(userVacationInfoService));
        }

        /// <summary>
        /// Personal bookings edit get method.
        /// </summary>
        /// <param name="startDate">Date after which bookings should be found.</param>
        /// <param name="category">Category in which bookings should be found.</param>
        /// <param name="status">Status on which bookings should be found.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(DateTime? startDate, string category, string status)
        {
            var userId = int.Parse(User.FindFirst(ClaimKeys.Id).Value);
            var personalBookings = await _bookingService.FindPersonalBookingsByDateAndCategoryAndStatus(
                userId,
                startDate,
                category,
                status);

            var model = new Models.PersonalBookings()
            {
                StartDate = startDate,
                Category = category,
                Status = status,
                Bookings = personalBookings,
            };
            return View(model);
        }

        /// <summary>
        /// Remove booking.
        /// </summary>
        /// <param name="id">Holiday Id.</param>
        /// <returns>Task.</returns>
        [HttpDelete]
        public async Task Cancel(Guid id)
        {
            var personalBookings = await _bookingService.FindPersonalBooking(id);

            var userVacationInfoDto = await _userVacationInfoService.FindByUserId(personalBookings.UserId);
            var days = Convert.ToUInt32((personalBookings.Expires - personalBookings.Start).TotalDays);

            await _userVacationInfoService.AfterDeleteBookingUpdate(days, personalBookings.Category, userVacationInfoDto);
            await _bookingService.Remove(id);
        }
    }
}