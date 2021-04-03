using System.Threading.Tasks;
using AutoMapper;
using VTS.Repos.UnitOfWork;

namespace VTS.Services.HeadService
{
    /// <summary>
    /// Service for head logic.
    /// </summary>
    public class HeadService : IHeadService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="HeadService"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of work.</param>
        public HeadService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc/>
        public async Task<Core.DTO.Head> FindHeadByUserId(int userId)
        {
            var head = await _unitOfWork.Heads.FindHeadByUserId(userId);
            var headDto = _mapper.Map<Core.DTO.Head>(head);
            return headDto;
        }
    }
}
