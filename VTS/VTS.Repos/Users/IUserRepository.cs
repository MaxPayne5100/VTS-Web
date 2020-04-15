using System.Threading.Tasks;
using VTS.DAL.Entities;
using VTS.Repos.Generic;

namespace VTS.Repos.Users
{
    /// <summary>
    /// Interface for User Repository.
    /// </summary>
    public interface IUserRepository : IGenericRepository<User, uint>
    {
        /// <summary>
        /// Find User by Email.
        /// </summary>
        /// <param name="email">Email address.</param>
        /// <returns>User.</returns>
        Task<User> FindByEmail(string email);
    }
}