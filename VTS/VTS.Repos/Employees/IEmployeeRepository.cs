using System.Collections.Generic;
using System.Threading.Tasks;
using VTS.DAL.Entities;
using VTS.Repos.Generic;

namespace VTS.Repos.Employees
{
    /// <summary>
    /// Interface for Employee Repository.
    /// </summary>
    public interface IEmployeeRepository : IGenericRepository<Employee, uint>
    {
        /// <summary>
        /// Find list of Employees by Manager Id.
        /// </summary>
        /// <param name="id">Manager id.</param>
        /// <returns>List of Employees.</returns>
        Task<List<Employee>> FindByManagerId(uint id);
    }
}
