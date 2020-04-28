using System;
using System.Threading.Tasks;
using AutoMapper;
using VTS.Repos.UnitOfWork;

namespace VTS.Services.UserVacationInfoService
{
    /// <summary>
    /// Service for user vacation info logic.
    /// </summary>
    public class UserVacationInfoService : IUserVacationInfoService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserVacationInfoService"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of work.</param>
        public UserVacationInfoService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public async Task<Core.DTO.UserVacationInfo> FindByUserId(uint userId)
        {
            var usersVacationInfo = await _unitOfWork.UsersVacationInfo.FindByUserId(userId);
            var usersVacationInfoDtos = _mapper.Map<Core.DTO.UserVacationInfo>(usersVacationInfo);
            return usersVacationInfoDtos;
        }

        /// <inheritdoc />
        public async Task UpdateBonusDayOffs(Core.DTO.UserVacationInfo userVacationInfoDto)
        {
            var userVacationInfo = await _unitOfWork.UsersVacationInfo.FindByUserId(userVacationInfoDto.UserId);

            if (userVacationInfo != null)
            {
                userVacationInfo.BonusPaidDayOffs = userVacationInfoDto.BonusPaidDayOffs;

                _unitOfWork.UsersVacationInfo.Update(userVacationInfo);
                await _unitOfWork.CommitAsync();
            }
            else
            {
                throw new ArgumentException($"Can not find user with id {userVacationInfoDto.UserId}");
            }
        }
    }
}
