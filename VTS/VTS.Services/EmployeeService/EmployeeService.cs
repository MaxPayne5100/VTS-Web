using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using VTS.Repos.UnitOfWork;

namespace VTS.Services.EmployeeService
{
    /// <summary>
    /// Service for employee logic.
    /// </summary>
    public class EmployeeService : IEmployeeService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeService"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of work.</param>
        public EmployeeService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Core.DTO.Employee>> FindByManagerId(uint managerId)
        {
            var employees = await _unitOfWork.Employees.FindByManagerId(managerId);
            var employeesDto = _mapper.Map<List<Core.DTO.Employee>>(employees);
            return employeesDto;
        }
    }
}
