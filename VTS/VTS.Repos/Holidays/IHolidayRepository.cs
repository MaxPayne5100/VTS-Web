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
        /// <param name="startDate">Date after which booking should be found.</param>
        /// <returns>Task.</returns>
        Task<IEnumerable<Holiday>> FindPersonalBookingsByDate(int userId, DateTime? startDate);

        /// <summary>
        /// Find all holiday bookings by date.
        /// </summary>
        /// <param name="startDate">Date after which booking should be found.</param>
        /// <returns>Task.</returns>
        Task<IEnumerable<Holiday>> FindAllBookingsByDate(DateTime? startDate);

        /// <summary>
        /// Find personal booking by GUID identifier.
        /// </summary>
        /// <param name="id">Holiday identifier.</param>
        /// <returns>Task.</returns>
        Task<Holiday> FindPersonalBooking(Guid id);
    }
}