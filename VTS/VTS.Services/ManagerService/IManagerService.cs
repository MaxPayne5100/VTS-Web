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
        Task<Core.DTO.Manager> FindManageByUserId(uint userId);
    }
}