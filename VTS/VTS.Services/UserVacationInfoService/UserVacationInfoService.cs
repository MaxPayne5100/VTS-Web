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
        public async Task<Core.DTO.UserVacationInfo> FindByUserId(uint id)
        {
            var usersVacationInfo = await _unitOfWork.UsersVacationInfo.FindByUserId(id);
            var usersVacationInfoDtos = _mapper.Map<Core.DTO.UserVacationInfo>(usersVacationInfo);
            return usersVacationInfoDtos;
        }
    }
}
