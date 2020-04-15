using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VTS.Services.UserVacationInfoService;

namespace VTS.Web.ViewComponents
{
    /// <summary>
    /// ViewComponent for UserVacationInfo.
    /// </summary>
    public class UserVacationInfoViewComponent : ViewComponent
    {
        private readonly IMapper _mapper;
        private readonly IUserVacationInfoService _userVacationInfoService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserVacationInfoViewComponent"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="userVacationInfoService">Service for UserVacationInfo logic.</param>
        public UserVacationInfoViewComponent(IMapper mapper, IUserVacationInfoService userVacationInfoService)
        {
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
            _userVacationInfoService = userVacationInfoService ?? throw new ArgumentException(nameof(userVacationInfoService));
        }

        /// <summary>
        /// Method for calling view component.
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
