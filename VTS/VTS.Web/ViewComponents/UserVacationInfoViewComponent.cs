using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VTS.Services.UserVacationInfoService;

namespace VTS.Web.ViewComponents
{
    /// <summary>
    /// ViewComponent for UserVacationInfo.
    /// </summary>
    public class UserVacationInfoViewComponent : ViewComponent
    {
        private readonly IUserVacationInfoService _userVacationInfoService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserVacationInfoViewComponent"/> class.
        /// </summary>
        /// <param name="userVacationInfoService">Service for UserVacationInfo logic.</param>
        public UserVacationInfoViewComponent(IUserVacationInfoService userVacationInfoService)
        {
            _userVacationInfoService = userVacationInfoService ?? throw new ArgumentException(nameof(userVacationInfoService));
        }

        /// <summary>
        /// Method for calling view component (frontend part).
        /// </summary>
        /// <param name="id">User Id.</param>
        /// <returns>IViewComponentResult.</returns>
        public async Task<IViewComponentResult> InvokeAsync(uint id)
        {
            var usersVacationInfoDtos = await _userVacationInfoService.FindByUserId(id);
            return View(usersVacationInfoDtos);
        }
    }
}
