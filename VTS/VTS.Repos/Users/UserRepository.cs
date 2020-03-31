using VTS.DAL;
using VTS.DAL.Entities;
using VTS.Repos.Generic;

namespace VTS.Repos.Users
{
    public class UserRepository : GenericRepository<User, uint>, IUserRepository
    {
        private readonly VTSDbContext _dbContext;

        public UserRepository(VTSDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}