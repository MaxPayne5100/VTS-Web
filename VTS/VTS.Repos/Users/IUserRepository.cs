using System.Collections.Generic;
using System.Threading.Tasks;
using VTS.DAL.Entities;
using VTS.Repos.Generic;

namespace VTS.Repos.Users
{
    /// <summary>
    /// Interface for User Repository.
    /// </summary>
    public interface IUserRepository : IGenericRepository<User, int>
    {
        /// <summary>
        /// Find User by Email.
        /// </summary>
        /// <param name="email">Email address.</param>
        /// <returns>User.</returns>
        Task<User> FindByEmail(string email);

        /// <summary>
        /// Find user by role without own data.
        /// </summary>
        /// <param name="role">String role.</param>
        /// <param name="id">User id.</param>
        /// <returns>User list.</returns>
        Task<IEnumerable<User>> FindByRoleWithoutOwnData(string role, int id);

        /// <summary>
        /// Find user with all roles info by id.
        /// </summary>
        /// <param name="id">User identifier.</param>
        /// <returns>User.</returns>
        Task<User> FindWithAllRolesInfoById(int id);
    }
}