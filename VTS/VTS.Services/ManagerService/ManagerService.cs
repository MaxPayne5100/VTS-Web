using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using VTS.Repos.UnitOfWork;

namespace VTS.Services.ManagerService
{
    /// <summary>
    /// Service for manager logic.
    /// </summary>
    public class ManagerService : IManagerService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerService"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of work.</param>
        public ManagerService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public async Task<Core.DTO.Manager> FindManagerByUserId(int userId)
        {
            var manager = await _unitOfWork.Managers.FindManagerByUserId(userId);

            if (manager != null)
            {
                var managerDto = _mapper.Map<Core.DTO.Manager>(manager);
                return managerDto;
            }
            else
            {
                throw new ArgumentException($"Неможливо найти менеджера з id користувача {userId}");
            }
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Core.DTO.Manager>> GetAllWithUserInfo()
        {
            var managers = await _unitOfWork.Managers.GetAllWithUserInfo();

            if (managers != null)
            {
                var managersDtos = _mapper.Map<IEnumerable<Core.DTO.Manager>>(managers);
                return managersDtos;
            }
            else
            {
                return new List<Core.DTO.Manager>();
            }
        }
    }
}