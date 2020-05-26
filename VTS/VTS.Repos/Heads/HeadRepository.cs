using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VTS.DAL;
using VTS.DAL.Entities;
using VTS.Repos.Generic;

namespace VTS.Repos.Heads
{
    /// <summary>
    /// Clerk Repository.
    /// </summary>
    public class HeadRepository : GenericRepository<Head, int>, IHeadRepository
    {
        private readonly VTSDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="HeadRepository"/> class.
        /// </summary>
        /// <param name="dbContext">DbContext.</param>
        public HeadRepository(VTSDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<Head> FindHeadByUserId(int userId)
        {
            return await _dbContext.Heads.Include(x => x.User)
                                         .SingleOrDefaultAsync(x => x.UserId.Equals(userId));
        }
    }
}