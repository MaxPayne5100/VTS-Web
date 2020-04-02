using VTS.DAL.Entities;
using VTS.Repos.Generic;
using System.Threading.Tasks;

namespace VTS.Repos.Users
{
    public interface IUserRepository : IGenericRepository<User, uint> 
    {
        Task<User> FindByEmail(string email);
    }
}