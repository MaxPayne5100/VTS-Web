using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VTS.Services.BookingService;

namespace VTS.Web.ViewComponents
{
    /// <summary>
    /// View component for bookings list.
    /// </summary>
    public class FilteredBookingsViewComponent : ViewComponent
    {
        private readonly IBookingService _bookingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="FilteredBookingsViewComponent"/> class.
        /// </summary>
        /// <param name="bookingService">Booking service.</param>
        public FilteredBookingsViewComponent(IBookingService bookingService)
        {
            _bookingService = bookingService ?? throw new ArgumentException(nameof(bookingService));
        }

        /// <summary>
        /// Invoke view component.
        /// </summary>
        /// <param name="id">Holiday identifier.</param>
        /// <returns>IViewComponentResult.</returns>
        public async Task<IViewComponentResult> InvokeAsync(Guid id)
        {
            var bookingDto = await _bookingService.FindPersonalBooking(id);
            var model = new Models.ApproveBookingModel() { Id = bookingDto.Id, UserId = bookingDto.UserId };
            return View(model);
        }
    }
}