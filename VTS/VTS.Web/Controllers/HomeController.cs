using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VTS.Core.Constants;
using VTS.Services.BookingService;
using VTS.Services.UserVacationInfoService;
using VTS.Web.Models;

namespace VTS.Web.Controllers
{
    /// <summary>
    /// Home controller.
    /// </summary>
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBookingService _bookingService;
        private readonly IUserVacationInfoService _userVacationInfoService;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="mapper">Mapper.</param>
        /// <param name="bookingService">Booking service.</param>
        /// <param name="userVacationInfoService">UserVacationInfo service.</param>
        public HomeController(IMapper mapper, IBookingService bookingService, IUserVacationInfoService userVacationInfoService)
        {
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
            _bookingService = bookingService ?? throw new ArgumentException(nameof(bookingService));
            _userVacationInfoService = userVacationInfoService ?? throw new ArgumentException(nameof(userVacationInfoService));
        }

        /// <summary>
        /// Get method for home page.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Post method for home page.
        /// </summary>
        /// <param name="model">Booking model.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        public async Task<IActionResult> Index(BookingModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var modelDto = _mapper.Map<Core.DTO.Holiday>(model);
                    modelDto.UserId = int.Parse(User.FindFirst(ClaimKeys.Id).Value);

                    var userVacationInfoDto = await _userVacationInfoService.FindByUserId(modelDto.UserId);
                    var days = Convert.ToUInt32((modelDto.Expires - modelDto.Start).TotalDays);

                    await _userVacationInfoService.AfterBookingUpdate(days, modelDto.Category, modelDto.Start, userVacationInfoDto);
                    await _bookingService.Add(modelDto);
                    return RedirectToAction("Index");
                }
                catch (ArgumentException e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }
    }
}