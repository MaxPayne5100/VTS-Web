using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VTS.Services.EmployeeService;

namespace VTS.Web.ViewComponents
{
    /// <summary>
    /// ViewComponent for ManagerEmployees.
    /// </summary>
    public class ManagerEmployeesViewComponent : ViewComponent
    {
        private readonly IEmployeeService _employeeService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerEmployeesViewComponent"/> class.
        /// </summary>
        /// <param name="employeeService">Service for Employee logic.</param>
        public ManagerEmployeesViewComponent(IEmployeeService employeeService)
        {
            _employeeService = employeeService ?? throw new ArgumentException(nameof(employeeService));
        }

        /// <summary>
        /// Method for calling view component (frontend part).
        /// </summary>
        /// <param name="id">Manager Id.</param>
        /// <returns>IViewComponentResult.</returns>
        public async Task<IViewComponentResult> InvokeAsync(uint id)
        {
            var employeesDtos = await _employeeService.FindByManagerId(id);
            return View(employeesDtos);
        }
    }
}