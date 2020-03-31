using VTS.DAL.Entities;
using VTS.Repos.Generic;

namespace VTS.Repos.Users
{
    public interface IUserRepository : IGenericRepository<User, uint> { }
}