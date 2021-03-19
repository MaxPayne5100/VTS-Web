using System.Collections.Generic;
using System.Threading.Tasks;

namespace VTS.Services.ManagerService
{
    /// <summary>
    /// Interface for Manager service.
    /// </summary>
    public interface IManagerService
    {
        /// <summary>
        /// Asynchronous find manager by user's id.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <returns>Manager.</returns>
        Task<Core.DTO.Manager> FindManagerByUserId(int userId);

        /// <summary>
        /// Get all managers.
        /// </summary>
        /// <returns>IEnumerable of managers.</returns>
        Task<IEnumerable<Core.DTO.Manager>> GetAllWithUserInfo();
    }
}