using System;
using System.Threading.Tasks;
using AutoMapper;
using VTS.Core.Constants;
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

        private string MapToEnum(string dtoCategory)
        {
            if (dtoCategory == "Оплачувана відпустка")
            {
                return "PaidDayOffs";
            }
            else if (dtoCategory == "Неоплачувана відпустка")
            {
                return "UnPaidDayOffs";
            }
            else if (dtoCategory == "Оплачуваний лікарняний")
            {
                return "PaidSickness";
            }
            else if (dtoCategory == "Неоплачуваний лікарняний")
            {
                return "UnPaidSickness";
            }
            else
            {
                return dtoCategory;
            }
        }

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
            var usersVacationInfoDto = _mapper.Map<Core.DTO.UserVacationInfo>(usersVacationInfo);
            return usersVacationInfoDto;
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

        /// <inheritdoc />
        public async Task AfterBookingUpdate(uint days, string category, DateTime start, Core.DTO.UserVacationInfo userVacationInfoDto)
        {
            var userVacationInfo = await _unitOfWork.UsersVacationInfo.FindByUserId(userVacationInfoDto.UserId);

            category = MapToEnum(category);

            if (userVacationInfo != null)
            {
                var months = (start - userVacationInfoDto.StartedWorking).TotalDays;
                months /= GeneralConstants.DaysToMonths;

                if (months < CompanyPolicy.MinExpInCompany)
                {
                    throw new ArgumentException($"У вас замало досвіду в компанії щоб замовити відпустку");
                }

                if (category == VacationCategories.PaidDayOffs)
                {
                    if (days > userVacationInfo.PaidDayOffs)
                    {
                        throw new ArgumentException($"Забагато днів відпустки");
                    }
                    else
                    {
                        userVacationInfoDto.PaidDayOffs -= days;
                        userVacationInfo.PaidDayOffs = userVacationInfoDto.PaidDayOffs;
                    }
                }
                else if (category == VacationCategories.UnPaidDayOffs)
                {
                    if (days > userVacationInfo.UnPaidDayOffs)
                    {
                        throw new ArgumentException($"Забагато днів відпустки");
                    }
                    else
                    {
                        userVacationInfoDto.UnPaidDayOffs -= days;
                        userVacationInfo.UnPaidDayOffs = userVacationInfoDto.UnPaidDayOffs;
                    }
                }
                else if (category == VacationCategories.PaidSickness)
                {
                    if (days > userVacationInfo.PaidSickness)
                    {
                        throw new ArgumentException($"Забагато днів лікарняного");
                    }
                    else
                    {
                        userVacationInfoDto.PaidSickness -= days;
                        userVacationInfo.PaidSickness = userVacationInfoDto.PaidSickness;
                    }
                }
                else if (category == VacationCategories.UnPaidSickness)
                {
                    if (days > userVacationInfo.UnPaidSickness)
                    {
                        throw new ArgumentException($"Забагато днів лікарняного");
                    }
                    else
                    {
                        userVacationInfoDto.UnPaidSickness -= days;
                        userVacationInfo.UnPaidSickness = userVacationInfoDto.UnPaidSickness;
                    }
                }

                _unitOfWork.UsersVacationInfo.Update(userVacationInfo);
                await _unitOfWork.CommitAsync();
            }
            else
            {
                throw new ArgumentException($"Не знайдено ваші дані про відпуск");
            }
        }

        /// <inheritdoc />
        public async Task AfterDeleteBookingUpdate(uint days, string category, Core.DTO.UserVacationInfo userVacationInfoDto)
        {
            var userVacationInfo = await _unitOfWork.UsersVacationInfo.FindByUserId(userVacationInfoDto.UserId);

            category = MapToEnum(category);

            if (userVacationInfo != null)
            {
                if (category == VacationCategories.PaidDayOffs)
                {
                    userVacationInfoDto.PaidDayOffs += days;
                    userVacationInfo.PaidDayOffs = userVacationInfoDto.PaidDayOffs;
                }
                else if (category == VacationCategories.UnPaidDayOffs)
                {
                    userVacationInfoDto.UnPaidDayOffs += days;
                    userVacationInfo.UnPaidDayOffs = userVacationInfoDto.UnPaidDayOffs;
                }
                else if (category == VacationCategories.PaidSickness)
                {
                    userVacationInfoDto.PaidSickness += days;
                    userVacationInfo.PaidSickness = userVacationInfoDto.PaidSickness;
                }
                else if (category == VacationCategories.UnPaidSickness)
                {
                    userVacationInfoDto.UnPaidSickness += days;
                    userVacationInfo.UnPaidSickness = userVacationInfoDto.UnPaidSickness;
                }

                _unitOfWork.UsersVacationInfo.Update(userVacationInfo);
                await _unitOfWork.CommitAsync();
            }
            else
            {
                throw new ArgumentException($"Не знайдено ваші дані про відпуск");
            }
        }
    }
}
