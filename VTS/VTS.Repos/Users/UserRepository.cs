using VTS.DAL;
using VTS.DAL.Entities;
using VTS.Repos.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace VTS.Repos.Users
{
    public class UserRepository : GenericRepository<User, uint>, IUserRepository
    {
        private readonly VTSDbContext _dbContext;

        public UserRepository(VTSDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> FindByEmail(string email)
        {
            return await _dbContext.Users
                .FirstOrDefaultAsync(user => user.Email.Equals(email));
        }
    }
}