using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VTS.Core.Constants;
using VTS.Services.UserService;
using VTS.Services.UserVacationInfoService;
using VTS.Web.Models;

namespace VTS.Web.Controllers
{
    /// <summary>
    /// Controller for Clerk role.
    /// </summary>
    [Authorize(Policy = Policies.ClerkOnly)]
    public class ClerkController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IUserVacationInfoService _userVacationInfoService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClerkController"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="userService">Service for user logic.</param>
        /// <param name="userVacationInfoService">UserVacationInfo service.</param>
        public ClerkController(IMapper mapper, IUserService userService, IUserVacationInfoService userVacationInfoService)
        {
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
            _userService = userService ?? throw new ArgumentException(nameof(userService));
            _userVacationInfoService = userVacationInfoService ?? throw new ArgumentException(nameof(userVacationInfoService));
    }

        /// <summary>
        /// Get method for calling users edit page.
        /// </summary>
        /// <param name="role">User role.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        public async Task<IActionResult> UsersEdit(string role = "Employee")
        {
            var id = int.Parse(User.FindFirst(ClaimKeys.Id).Value);
            var users = await _userService.FindByRoleWithoutOwnData(role, id);
            var model = new EditUsersModel() { Role = role, Users = users };
            return View(model);
        }

        /// <summary>
        /// Delete user method.
        /// </summary>
        /// <param name="id">User Identifier.</param>
        /// <returns>Task.</returns>
        [HttpDelete]
        public async Task RemoveUser(int id)
        {
            await _userService.Remove(id);
        }

        /// <summary>
        /// Add user get method.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        public IActionResult AddUser()
        {
            return PartialView("_AddUserPartial");
        }

        /// <summary>
        /// Add user post method.
        /// </summary>
        /// <param name="model">UserModel object.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        public async Task<IActionResult> AddUser(UserModel model)
        {
            if (model.ManagerId == null && model.Role == Roles.Employee)
            {
                ModelState.AddModelError("ManagerId", "Потрібно вказати менеджера для працівника");
                return PartialView("_AddUserPartial", model);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var userDto = _mapper.Map<Core.DTO.User>(model);
                    await _userService.Add(userDto);
                    return PartialView("_AddUserPartial", model);
                }
                catch (ArgumentException e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                    return PartialView("_AddUserPartial", model);
                }
            }
            else
            {
                return PartialView("_AddUserPartial", model);
            }
        }

        /// <summary>
        /// Asynchronous change vacation info get method.
        /// </summary>
        /// <param name="id">User Id.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        public async Task<IActionResult> ChangeVacationInfo(int id)
        {
            var usersVacationInfoDtos = await _userVacationInfoService.FindByUserId(id);
            var changeBonusDayOffsModel = _mapper.Map<ChangeDayOffsModel>(usersVacationInfoDtos);

            if (changeBonusDayOffsModel == null)
            {
                changeBonusDayOffsModel = new ChangeDayOffsModel { UserId = id };
            }

            return PartialView("_ChangeVacationInfo", changeBonusDayOffsModel);
        }

        /// <summary>
        /// Asynchronous change vacation info post method.
        /// </summary>
        /// <param name="dayOffs">ChangeDayOffs model.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        public async Task<IActionResult> ChangeVacationInfo(ChangeDayOffsModel dayOffs)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usersVacationInfoDtos = _mapper.Map<Core.DTO.UserVacationInfo>(dayOffs);
                    await _userVacationInfoService.UpdateVacationInfo(usersVacationInfoDtos);
                    return PartialView("_ChangeVacationInfo", dayOffs);
                }
                catch (ArgumentException e)
                {
                    ViewBag.Error = e.Message;
                    return PartialView("_ChangeVacationInfo", dayOffs);
                }
            }
            else
            {
                return PartialView("_ChangeVacationInfo", dayOffs);
            }
        }

        /// <summary>
        /// Get method for calling user edit modal page.
        /// </summary>
        /// <param name="id">User Identifier.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        public async Task<IActionResult> EditUser(int id)
        {
            var userDto = await _userService.FindWithManagerInfoById(id);
            var userModel = _mapper.Map<UserModel>(userDto);

            return PartialView("_EditUserPartial", userModel);
        }

        /// <summary>
        /// Post method for user edit.
        /// </summary>
        /// <param name="model">UserModel.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        public async Task<IActionResult> EditUser(UserModel model)
        {
            if (model.ManagerId == null && model.Role == Roles.Employee)
            {
                ModelState.AddModelError("ManagerId", "Потрібно вказати менеджера для працівника");
                return PartialView("_EditUserPartial", model);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var userDto = _mapper.Map<Core.DTO.User>(model);
                    await _userService.Update(userDto);
                    return PartialView("_EditUserPartial", model);
                }
                catch (ArgumentException e)
                {
                    ViewBag.Error = e.Message;
                    return PartialView("_EditUserPartial", model);
                }
            }
            else
            {
                return PartialView("_EditUserPartial", model);
            }
        }
    }
}