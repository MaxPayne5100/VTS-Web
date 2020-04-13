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
    public class UserRepository : GenericRepository<User, uint>, IUserRepository
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
    }
}