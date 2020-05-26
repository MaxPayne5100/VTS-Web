using System;
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
        Task<Core.DTO.UserVacationInfo> FindByUserId(int userId);

        /// <summary>
        /// Asynchronous update user's bonus paid day offs.
        /// </summary>
        /// <param name="userVacationInfoDto">Updated UserVacationInfo data.</param>
        /// <returns>Task.</returns>
        Task UpdateBonusDayOffs(Core.DTO.UserVacationInfo userVacationInfoDto);

        /// <summary>
        /// Asynchronous update user's vacation info.
        /// </summary>
        /// <param name="userVacationInfoDto">Updated UserVacationInfo data.</param>
        /// <returns>Task.</returns>
        Task UpdateVacationInfo(Core.DTO.UserVacationInfo userVacationInfoDto);

        /// <summary>
        /// Asynchronous update user's vacation info after booking.
        /// </summary>
        /// <param name="days">Days of holiday.</param>
        /// <param name="category">Category.</param>
        /// <param name="start">Start of holiday.</param>
        /// <param name="userVacationInfoDto">UserVacationInfo data.</param>
        /// <returns>Task.</returns>
        Task AfterBookingUpdate(uint days, string category, DateTime start, Core.DTO.UserVacationInfo userVacationInfoDto);

        /// <summary>
        /// Asynchronous update user's vacation info after delete booking.
        /// </summary>
        /// <param name="days">Days of holiday.</param>
        /// <param name="category">Category.</param>
        /// <param name="userVacationInfoDto">UserVacationInfo data.</param>
        /// <returns>Task.</returns>
        Task AfterDeleteBookingUpdate(uint days, string category, Core.DTO.UserVacationInfo userVacationInfoDto);
    }
}
