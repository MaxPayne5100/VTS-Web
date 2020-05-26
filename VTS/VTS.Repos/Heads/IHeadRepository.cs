using System.Threading.Tasks;
using VTS.DAL.Entities;
using VTS.Repos.Generic;

namespace VTS.Repos.Heads
{
    /// <summary>
    /// Interface for Head Repository.
    /// </summary>
    public interface IHeadRepository : IGenericRepository<Head, int>
    {
        /// <summary>
        /// Find head by user id.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <returns>Head.</returns>
        Task<Head> FindHeadByUserId(int userId);
    }
}