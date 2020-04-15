using System;
using System.Threading.Tasks;
using VTS.DAL.Entities;
using VTS.Repos.Generic;

namespace VTS.Repos.UsersVacationInfo
{
    /// <summary>
    /// Interface for UserVacationInfo Repository.
    /// </summary>
    public interface IUserVacationInfoRepository : IGenericRepository<UserVacationInfo, Guid>
    {
        /// <summary>
        /// Find UserVacationInfo by User Id.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>UserVacationInfo.</returns>
        Task<UserVacationInfo> FindByUserId(uint id);
    }
}