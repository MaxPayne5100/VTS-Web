using System.Collections.Generic;
using System.Threading.Tasks;

namespace VTS.Services.EmployeeService
{
    /// <summary>
    /// Interface for Employee service.
    /// </summary>
    public interface IEmployeeService
    {
        /// <summary>
        /// Asynchronous find employee by user's id.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <returns>Employee.</returns>
        Task<Core.DTO.Employee> FindEmployeeByUserId(int userId);

        /// <summary>
        /// Asynchronous find list of employees by manager's id.
        /// </summary>
        /// <param name="managerId">Manager Id.</param>
        /// <returns>List of Employees.</returns>
        Task<IEnumerable<Core.DTO.Employee>> FindByManagerId(int managerId);
    }
}
