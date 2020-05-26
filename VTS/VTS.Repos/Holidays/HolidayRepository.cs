using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VTS.DAL;
using VTS.DAL.Entities;
using VTS.Repos.Generic;

namespace VTS.Repos.Holidays
{
    /// <summary>
    /// Holiday Repository.
    /// </summary>
    public class HolidayRepository : GenericRepository<Holiday, Guid>, IHolidayRepository
    {
        private readonly VTSDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="HolidayRepository"/> class.
        /// </summary>
        /// <param name="dbContext">DbContext.</param>
        public HolidayRepository(VTSDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Holiday>> FindPersonalBookingsByDate(int userId, DateTime? startDate)
        {
            var bookings = _dbContext.Holidays.Include(x => x.HolidayAcception)
                                              .Where(x => x.UserId.Equals(userId));

            if (startDate != null)
            {
                bookings = bookings.Where(x => x.Start >= startDate);
            }

            return await bookings.OrderBy(x => x.SubmissionTime).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Holiday>> FindAllBookingsByDate(DateTime? startDate)
        {
            if (startDate != null)
            {
                var bookings = _dbContext.Holidays.Include(x => x.HolidayAcception)
                                                  .Include(x => x.User)
                                                  .Where(x => x.Start >= startDate && x.HolidayAcception == null);

                return await bookings.OrderBy(x => x.SubmissionTime).ToListAsync();
            }
            else
            {
                var bookings = _dbContext.Holidays.Include(x => x.HolidayAcception)
                                                  .Include(x => x.User)
                                                  .Where(x => x.HolidayAcception == null);

                return await bookings.OrderBy(x => x.SubmissionTime).ToListAsync();
            }
        }

        /// <inheritdoc/>
        public async Task<Holiday> FindPersonalBooking(Guid id)
        {
            return await _dbContext.Holidays.SingleOrDefaultAsync(x => x.Id.Equals(id));
        }
    }
}