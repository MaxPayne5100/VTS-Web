using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VTS.Core.Constants;
using VTS.Services.AuthenticationService;
using VTS.Services.UserService;

namespace VTS.Web.Controllers
{
    /// <summary>
    /// Controller for Profile.
    /// </summary>
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileController"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="userService">Service for user logic.</param>
        /// <param name="authService">Service for Authentication.</param>
        public ProfileController(IMapper mapper, IUserService userService, IAuthenticationService authService)
        {
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
            _userService = userService ?? throw new ArgumentException(nameof(userService));
            _authService = authService ?? throw new ArgumentException(nameof(authService));
        }

        /// <summary>
        /// Asynchronous edit get method.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var id = uint.Parse(User.FindFirst(ClaimKeys.Id).Value);
            var user = await _userService.Find(id);
            var profileModel = _mapper.Map<Models.ProfileModel>(user);
            return View(profileModel);
        }

        /// <summary>
        /// Asynchronous edit post method.
        /// </summary>
        /// <param name="profileModel">User profile model.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        public async Task<IActionResult> Edit(Models.ProfileModel profileModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userDto = _mapper.Map<Core.DTO.User>(profileModel);
                    userDto.Id = uint.Parse(User.FindFirst(ClaimKeys.Id).Value);
                    await _userService.UpdateProfile(userDto);
                    return View(profileModel);
                }
                catch (ArgumentException e)
                {
                    ViewBag.Error = e.Message;
                    return View(profileModel);
                }
            }
            else
            {
                return View(profileModel);
            }
        }
    }
}