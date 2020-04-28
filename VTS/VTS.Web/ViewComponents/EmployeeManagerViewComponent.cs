using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VTS.Services.EmployeeService;

namespace VTS.Web.ViewComponents
{
    /// <summary>
    /// ViewComponent for EmployeeManager.
    /// </summary>
    public class EmployeeManagerViewComponent : ViewComponent
    {
        private readonly IEmployeeService _employeeService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeManagerViewComponent"/> class.
        /// </summary>
        /// <param name="employeeService">Service for Employee logic.</param>
        public EmployeeManagerViewComponent(IEmployeeService employeeService)
        {
            _employeeService = employeeService ?? throw new ArgumentException(nameof(employeeService));
        }

        /// <summary>
        /// Method for calling view component (frontend part).
        /// </summary>
        /// <param name="id">User Id.</param>
        /// <returns>IViewComponentResult.</returns>
        public async Task<IViewComponentResult> InvokeAsync(uint id)
        {
            var employeeDtos = await _employeeService.FindEmployeeByUserId(id);
            return View(employeeDtos);
        }
    }
}