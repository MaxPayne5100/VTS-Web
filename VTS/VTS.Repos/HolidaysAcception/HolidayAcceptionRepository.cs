using System;
using VTS.DAL;
using VTS.DAL.Entities;
using VTS.Repos.Generic;

namespace VTS.Repos.HolidaysAcception
{
    /// <summary>
    /// HolidayAcception Repository.
    /// </summary>
    public class HolidayAcceptionRepository : GenericRepository<HolidayAcception, Guid>, IHolidayAcceptionRepository
    {
        private readonly VTSDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="HolidayAcceptionRepository"/> class.
        /// </summary>
        /// <param name="dbContext">DbContext.</param>
        public HolidayAcceptionRepository(VTSDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}