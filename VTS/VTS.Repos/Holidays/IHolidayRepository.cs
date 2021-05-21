using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VTS.DAL.Entities;
using VTS.Repos.Generic;

namespace VTS.Repos.Holidays
{
    /// <summary>
    /// Interface for Holiday Repository.
    /// </summary>
    public interface IHolidayRepository : IGenericRepository<Holiday, Guid>
    {
        /// <summary>
        /// Find personal bookings by user identifier and date.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="startDate">Date after which bookings should be found.</param>
        /// <returns>Task.</returns>
        Task<IEnumerable<Holiday>> FindPersonalBookingsByDate(int userId, DateTime? startDate);

        /// <summary>
        /// Find personal bookings by user identifier, date and category.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="startDate">Date after which bookings should be found.</param>
        /// <param name="category">Category in which bookings should be found.</param>
        /// <param name="status">Status on which bookings should be found.</param>
        /// <returns>Task.</returns>
        Task<IEnumerable<Holiday>> FindPersonalBookingsByDateAndCategoryAndStatus(
            int userId,
            DateTime? startDate,
            string category,
            string status);

        /// <summary>
        /// Find all holiday bookings by date.
        /// </summary>
        /// <param name="startDate">Date after which bookings should be found.</param>
        /// <returns>Task.</returns>
        Task<IEnumerable<Holiday>> FindAllBookingsByDate(DateTime? startDate);

        /// <summary>
        /// Find all holiday bookings by date and category.
        /// </summary>
        /// <param name="startDate">Date after which bookings should be found.</param>
        /// <param name="category">Category in which bookings should be found.</param>
        /// <returns>Task.</returns>
        Task<IEnumerable<Holiday>> FindAllBookingsWithIncludedInfo(DateTime? startDate, string category);

        /// <summary>
        /// Find all holiday bookings on the specified dates.
        /// </summary>
        /// <param name="startDate">Date after which bookings should be found.</param>
        /// <param name="endDate">Date by which bookings should be found.</param>
        /// <returns>Task.</returns>
        Task<IEnumerable<Holiday>> FindAllBookingsInDateRange(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Find personal booking by GUID identifier.
        /// </summary>
        /// <param name="id">Holiday identifier.</param>
        /// <returns>Task.</returns>
        Task<Holiday> FindPersonalBooking(Guid id);

        /// <summary>
        /// Find personal bookings on the specified dates.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="startDate">Date after which bookings should be found.</param>
        /// <param name="endDate">Date by which bookings should be found.</param>
        /// <returns>Task.</returns>
        Task<IEnumerable<Holiday>> FindPersonalBookingsInDateRange(
            int userId,
            DateTime startDate,
            DateTime endDate);
    }
}