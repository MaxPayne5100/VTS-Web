using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VTS.Services.ManagerService;

namespace VTS.Web.ViewComponents
{
    /// <summary>
    /// View component for managers list.
    /// </summary>
    public class ManagerOptionsViewComponent : ViewComponent
    {
        private readonly IManagerService _managerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerOptionsViewComponent"/> class.
        /// </summary>
        /// <param name="managerService">Manager service.</param>
        public ManagerOptionsViewComponent(IManagerService managerService)
        {
            _managerService = managerService ?? throw new ArgumentException(nameof(managerService));
        }

        /// <summary>
        /// Method for calling partial view.
        /// </summary>
        /// <param name="id">ManagerId.</param>
        /// <returns>IViewComponentResult.</returns>
        public async Task<IViewComponentResult> InvokeAsync(string id = "0")
        {
            var managers = await _managerService.GetAllWithUserInfo();
            var model = new Models.ManagerOptionsModel()
            {
                ManagerId = id,
                Managers = managers,
            };

            return View(model);
        }
    }
}