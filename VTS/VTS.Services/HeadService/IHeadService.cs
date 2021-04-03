using System.Threading.Tasks;

namespace VTS.Services.HeadService
{
    /// <summary>
    /// Interface for Head service.
    /// </summary>
    public interface IHeadService
    {
        /// <summary>
        /// Find head by user id.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <returns>Head.</returns>
        Task<Core.DTO.Head> FindHeadByUserId(int userId);
    }
}
