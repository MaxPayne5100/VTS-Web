using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VTS.DAL;
using VTS.DAL.Entities;
using VTS.Repos.Generic;

namespace VTS.Repos.Managers
{
    /// <summary>
    /// Manager Repository.
    /// </summary>
    public class ManagerRepository : GenericRepository<Manager, int>, IManagerRepository
    {
        private readonly VTSDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerRepository"/> class.
        /// </summary>
        /// <param name="dbContext">DbContext.</param>
        public ManagerRepository(VTSDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<Manager> FindManageByUserId(int userId)
        {
            return await _dbContext.Managers.Include(x => x.Head)
                                                .ThenInclude(y => y.User)
                                            .SingleOrDefaultAsync(x => x.Head.UserId.Equals(userId));
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Manager>> GetAllWithUserInfo()
        {
            return await _dbContext.Managers.Include(x => x.Head)
                                                .ThenInclude(y => y.User)
                                            .ToListAsync();
        }
    }
}
