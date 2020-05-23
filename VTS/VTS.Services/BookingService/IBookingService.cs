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
    }
}
