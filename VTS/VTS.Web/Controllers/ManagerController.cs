using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VTS.Core.Constants;
using VTS.Services.ManagerService;

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

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerController"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="managerService">Service for manager logic.</param>
        public ManagerController(IMapper mapper, IManagerService managerService)
        {
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
            _managerService = managerService ?? throw new ArgumentException(nameof(managerService));
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
    }
}