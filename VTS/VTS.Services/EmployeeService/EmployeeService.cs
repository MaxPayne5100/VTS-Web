using System;
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
        public async Task<Core.DTO.Employee> FindEmployeeByUserId(int userId)
        {
            var employee = await _unitOfWork.Employees.FindEmployeeByUserId(userId);

            if (employee != null)
            {
                var employeeDto = _mapper.Map<Core.DTO.Employee>(employee);
                return employeeDto;
            }
            else
            {
                throw new ArgumentException($"Неможливо найти працівника з id користувача {userId}");
            }
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Core.DTO.Employee>> FindByManagerId(int managerId)
        {
            var employees = await _unitOfWork.Employees.FindByManagerId(managerId);

            if (employees != null)
            {
                var employeesDtos = _mapper.Map<List<Core.DTO.Employee>>(employees);
                return employeesDtos;
            }
            else
            {
                return new List<Core.DTO.Employee>();
            }
        }
    }
}