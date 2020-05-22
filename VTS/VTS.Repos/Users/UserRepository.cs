using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VTS.DAL;
using VTS.DAL.Entities;
using VTS.Repos.Generic;

namespace VTS.Repos.Users
{
    /// <summary>
    /// User Repository.
    /// </summary>
    public class UserRepository : GenericRepository<User, int>, IUserRepository
    {
        private readonly VTSDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="dbContext">DbContext.</param>
        public UserRepository(VTSDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<User> FindByEmail(string email)
        {
            return await _dbContext.Users
                .FirstOrDefaultAsync(user => user.Email.Equals(email));
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<User>> FindByRoleWithoutOwnData(string role, int id)
        {
            return await _dbContext.Users.Where(x => x.Role == role && x.Id != id)
                                         .OrderBy(x => x.LastName).ThenBy(y => y.FirstName)
                                         .ToListAsync();
        }

        /// <inheritdoc />
        public async Task<User> FindWithAllRolesInfoById(int id)
        {
            return await _dbContext.Users.Include(x => x.Head)
                                            .ThenInclude(x => x.Manager)
                                         .Include(x => x.Head)
                                            .ThenInclude(x => x.Clerk)
                                         .Include(x => x.Employee)
                                         .Where(x => x.Id == id)
                                         .SingleOrDefaultAsync();
        }
    }
}