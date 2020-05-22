using VTS.DAL;
using VTS.DAL.Entities;
using VTS.Repos.Generic;

namespace VTS.Repos.Clerks
{
    /// <summary>
    /// Clerk Repository.
    /// </summary>
    public class ClerkRepository : GenericRepository<Clerk, int>, IClerkRepository
    {
        private readonly VTSDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClerkRepository"/> class.
        /// </summary>
        /// <param name="dbContext">DbContext.</param>
        public ClerkRepository(VTSDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}