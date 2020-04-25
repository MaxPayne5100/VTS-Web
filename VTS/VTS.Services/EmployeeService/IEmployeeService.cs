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
        /// Asynchronous find list of employees by manager's id.
        /// </summary>
        /// <param name="managerId">Manager Id.</param>
        /// <returns>List of Employees.</returns>
        Task<IEnumerable<Core.DTO.Employee>> FindByManagerId(uint managerId);
    }
}
