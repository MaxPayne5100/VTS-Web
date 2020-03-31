using VTS.DAL;
using VTS.DAL.Entities;

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