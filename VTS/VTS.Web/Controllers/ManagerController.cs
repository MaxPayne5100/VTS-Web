using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VTS.Core.Constants;
using VTS.Services.ManagerService;
using VTS.Services.UserVacationInfoService;

namespace VTS.Web.Controllers
{
    /// <summary>
    /// Controller for Manager role.
    /// </summary>
    [Authorize(Policy = Policies.ManagerOnly)]
    public class ManagerController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IManagerService _managerService;
        private readonly IUserVacationInfoService _userVacationInfoService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerController"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="managerService">Service for manager logic.</param>
        /// <param name="userVacationInfoService">UserVacationInfo service.</param>
        public ManagerController(IMapper mapper, IManagerService managerService, IUserVacationInfoService userVacationInfoService)
        {
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
            _managerService = managerService ?? throw new ArgumentException(nameof(managerService));
            _userVacationInfoService = userVacationInfoService ?? throw new ArgumentException(nameof(userVacationInfoService));
        }

        /// <summary>
        /// Asynchronous users edit get method.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        public async Task<IActionResult> UsersEdit()
        {
            var userId = uint.Parse(User.FindFirst(ClaimKeys.Id).Value);
            var managerDto = await _managerService.FindManageByUserId(userId);
            return View(managerDto.Id);
        }

        /// <summary>
        /// Asynchronous change bonus day offs get method.
        /// </summary>
        /// <param name="id">User Id.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        public async Task<IActionResult> ChangeBonusDayOffs(uint id)
        {
            var usersVacationInfoDtos = await _userVacationInfoService.FindByUserId(id);
            var changeBonusDayOffsModel = _mapper.Map<Models.ChangeBonusDayOffsModel>(usersVacationInfoDtos);
            return PartialView("_ChangeBonusDayOffs", changeBonusDayOffsModel);
        }

        /// <summary>
        /// Asynchronous change bonus day offs post method.
        /// </summary>
        /// <param name="bonusDayOffs">ChangeBonusDayOffs model.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        public async Task<IActionResult> ChangeBonusDayOffs(Models.ChangeBonusDayOffsModel bonusDayOffs)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usersVacationInfoDtos = _mapper.Map<Core.DTO.UserVacationInfo>(bonusDayOffs);
                    await _userVacationInfoService.UpdateBonusDayOffs(usersVacationInfoDtos);
                    return PartialView("_ChangeBonusDayOffs", bonusDayOffs);
                }
                catch (ArgumentException e)
                {
                    ViewBag.Error = e.Message;
                    return PartialView("_ChangeBonusDayOffs", bonusDayOffs);
                }
            }
            else
            {
                return PartialView("_ChangeBonusDayOffs", bonusDayOffs);
            }
        }
    }
}