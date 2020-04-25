using System.Threading.Tasks;

namespace VTS.Services.UserVacationInfoService
{
    /// <summary>
    /// Interface for UserVacationInfo service.
    /// </summary>
    public interface IUserVacationInfoService
    {
        /// <summary>
        /// Asynchronous find user's vacation info by user's id.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <returns>UsersVacationInfo.</returns>
        Task<Core.DTO.UserVacationInfo> FindByUserId(uint userId);
    }
}
