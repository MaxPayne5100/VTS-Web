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
        private readonly IMapper _mapper;
        private readonly IBookingService _bookingService;
        private readonly IUserVacationInfoService _userVacationInfoService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonalBookingController"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="bookingService">Booking service.</param>
        /// <param name="userVacationInfoService">UserVacationInfo service.</param>
        public PersonalBookingController(IMapper mapper, IBookingService bookingService, IUserVacationInfoService userVacationInfoService)
        {
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
            _bookingService = bookingService ?? throw new ArgumentException(nameof(bookingService));
            _userVacationInfoService = userVacationInfoService ?? throw new ArgumentException(nameof(userVacationInfoService));
        }

        /// <summary>
        /// Personal bookings edit get method.
        /// </summary>
        /// <param name="startDate">Date after which booking should be found.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(DateTime? startDate)
        {
            var userId = int.Parse(User.FindFirst(ClaimKeys.Id).Value);
            var personalBookings = await _bookingService.FindPersonalBookingsByDate(userId, startDate);
            var model = new Models.PersonalBookings() { StartDate = startDate, Bookings = personalBookings };
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