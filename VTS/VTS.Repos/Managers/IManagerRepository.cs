using System.Threading.Tasks;
using VTS.DAL.Entities;
using VTS.Repos.Generic;

namespace VTS.Repos.Managers
{
    /// <summary>
    /// Interface for Manager Repository.
    /// </summary>
    public interface IManagerRepository : IGenericRepository<Manager, uint>
    {
        /// <summary>
        /// Find Manager by User Id.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <returns>Manager.</returns>
        Task<Manager> FindManageByUserId(uint userId);
    }
}