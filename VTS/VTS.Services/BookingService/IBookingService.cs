using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VTS.Services.BookingService
{
    /// <summary>
    /// Interface for Booking service.
    /// </summary>
    public interface IBookingService
    {
        /// <summary>
        /// Add new booking.
        /// </summary>
        /// <param name="holidayDto">Booking.</param>
        /// <returns>Task.</returns>
        Task Add(Core.DTO.Holiday holidayDto);

        /// <summary>
        /// Add new booking approval.
        /// </summary>
        /// <param name="holidayaccDto">Booking approval.</param>
        /// <returns>Task.</returns>
        Task Approve(Core.DTO.HolidayAcception holidayaccDto);

        /// <summary>
        /// Find personal bookings by user identifier and date.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="startDate">Date after which booking should be found.</param>
        /// <returns>Holiday Dtos.</returns>
        Task<IEnumerable<Core.DTO.Holiday>> FindPersonalBookingsByDate(int userId, DateTime? startDate);

        /// <summary>
        /// Find all holiday bookings by date.
        /// </summary>
        /// <param name="startDate">Date after which booking should be found.</param>
        /// <returns>Holiday Dtos.</returns>
        Task<IEnumerable<Core.DTO.Holiday>> FindAllBookingsByDate(DateTime? startDate);

        /// <summary>
        /// Find personal booking by GUID identifier.
        /// </summary>
        /// <param name="id">Holiday identifier.</param>
        /// <returns>Holiday Dto.</returns>
        Task<Core.DTO.Holiday> FindPersonalBooking(Guid id);

        /// <summary>
        /// Removes Holiday with specified id.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <returns>Task.</returns>
        Task Remove(Guid id);
    }
}
