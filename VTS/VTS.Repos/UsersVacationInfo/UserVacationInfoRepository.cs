using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VTS.DAL;
using VTS.DAL.Entities;
using VTS.Repos.Generic;

namespace VTS.Repos.UsersVacationInfo
{
    /// <summary>
    /// UsersVacationInfo Repository.
    /// </summary>
    public class UserVacationInfoRepository : GenericRepository<UserVacationInfo, Guid>, IUserVacationInfoRepository
    {
        private readonly VTSDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserVacationInfoRepository"/> class.
        /// </summary>
        /// <param name="dbContext">DbContext.</param>
        public UserVacationInfoRepository(VTSDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<UserVacationInfo> FindByUserId(uint id)
        {
            return await _dbContext.UsersVacationInfo
                .SingleOrDefaultAsync(x => x.User.Id.Equals(id));
        }
    }
}
