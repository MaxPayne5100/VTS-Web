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
        public async Task<Core.DTO.UserVacationInfo> FindByUserId(int userId)
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
                throw new ArgumentException($"Неможливо знайти користувача з id {userVacationInfoDto.UserId}");
            }
        }

        /// <inheritdoc />
        public async Task UpdateVacationInfo(Core.DTO.UserVacationInfo userVacationInfoDto)
        {
            var userVacationInfo = await _unitOfWork.UsersVacationInfo.FindByUserId(userVacationInfoDto.UserId);

            if (userVacationInfo != null)
            {
                userVacationInfo.PaidDayOffs = userVacationInfoDto.PaidDayOffs;
                userVacationInfo.UnPaidDayOffs = userVacationInfoDto.UnPaidDayOffs;
                userVacationInfo.PaidSickness = userVacationInfoDto.PaidSickness;
                userVacationInfo.UnPaidSickness = userVacationInfoDto.UnPaidSickness;
                userVacationInfo.BonusPaidDayOffs = userVacationInfoDto.BonusPaidDayOffs;
                userVacationInfo.StartedWorking = userVacationInfoDto.StartedWorking;

                _unitOfWork.UsersVacationInfo.Update(userVacationInfo);
                await _unitOfWork.CommitAsync();
            }
            else
            {
                userVacationInfo = _mapper.Map<DAL.Entities.UserVacationInfo>(userVacationInfoDto);

                await _unitOfWork.UsersVacationInfo.AddAsync(userVacationInfo);
                await _unitOfWork.CommitAsync();
            }
        }
    }
}
