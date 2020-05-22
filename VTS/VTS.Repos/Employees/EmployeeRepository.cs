using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VTS.DAL;
using VTS.DAL.Entities;
using VTS.Repos.Generic;

namespace VTS.Repos.Employees
{
    /// <summary>
    /// Employee Repository.
    /// </summary>
    public class EmployeeRepository : GenericRepository<Employee, int>, IEmployeeRepository
    {
        private readonly VTSDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeRepository"/> class.
        /// </summary>
        /// <param name="dbContext">DbContext.</param>
        public EmployeeRepository(VTSDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<Employee> FindEmployeeByUserId(int userId)
        {
            return await _dbContext.Employees.Include(x => x.Manager)
                                                .ThenInclude(y => y.Head)
                                                    .ThenInclude(z => z.User)
                                            .SingleOrDefaultAsync(x => x.UserId.Equals(userId));
        }

        /// <inheritdoc />
        public async Task<List<Employee>> FindByManagerId(int id)
        {
            return await _dbContext.Employees.Include(x => x.User)
                                                .ThenInclude(y => y.UserVacationInfo)
                                              .Where(x => x.ManagerId == id)
                                              .OrderBy(x => x.User.LastName).ThenBy(x => x.User.FirstName)
                                              .ToListAsync();
        }
    }
}
