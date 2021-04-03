using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using NUnit.Framework;
using VTS.DAL.Entities;
using VTS.Repos.Holidays;
using VTS.Repos.HolidaysAcception;
using VTS.Repos.UnitOfWork;
using VTS.Services;
using VTS.Services.BookingService;

namespace VTS.Tests
{
    /// <summary>
    /// Tests for booking logic service.
    /// </summary>
    public class BookingServiceTests
    {
        private IBookingService _service;

        private IMapper _mapper;

        private Mock<IHolidayRepository> _repositoryMock;
        private Mock<IHolidayAcceptionRepository> _holidayacceptionRepositoryMock;

        private Mock<IUnitOfWork> _unitOfWorkMock;

        private User _registeredUser;
        private User _newUser;
        private Holiday _registeredholiday;
        private Holiday _newUserholiday;
        private HolidayAcception _registeredholidayacception;
        private HolidayAcception _newUserholidayacception;

        /// <summary>
        /// Method that is called once prior to executing any of the tests in a fixture.
        /// </summary>
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var serviceMapProfile = new ServiceMapProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(serviceMapProfile));
            _mapper = new Mapper(configuration);
        }

        /// <summary>
        /// Method that is used to provide a common set of functions that are performed just before each test method is called.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            _registeredUser = new User()
            {
                Id = 123,
                FirstName = "Firstname",
                LastName = "Lastname",
                Email = "registered@gmail.com",
            };

            _newUser = new User()
            {
                Id = 111,
                FirstName = "Firstname",
                LastName = "Lastname",
                Email = "new@gmail.com",
            };

            _registeredholiday = new Holiday()
            {
                Id = Guid.NewGuid(),
                UserId = _registeredUser.Id,
                User = _registeredUser,
                Category = Holiday.Categories.PaidDayOffs,
                Hours = 72,
                Start = DateTime.Today.AddDays(7),
                Expires = DateTime.Today.AddDays(10),
                Description = "Mind refresh",
                HolidayAcception = _registeredholidayacception,
            };

            _newUserholiday = new Holiday()
            {
                Id = Guid.NewGuid(),
                UserId = _newUser.Id,
                User = _newUser,
                Category = Holiday.Categories.PaidSickness,
                Hours = 96,
                Start = DateTime.Today.AddDays(5),
                Expires = DateTime.Today.AddDays(9),
                Description = "Flu",
                HolidayAcception = _newUserholidayacception,
            };

            _registeredholidayacception = new HolidayAcception()
            {
                Id = Guid.NewGuid(),
                HolidayId = _registeredholiday.Id,
                Holiday = _registeredholiday,
                Status = HolidayAcception.StatusType.Approved,
                Description = "Have a good time :)",
            };

            _newUserholidayacception = new HolidayAcception()
            {
                Id = Guid.NewGuid(),
                HolidayId = _newUserholiday.Id,
                Holiday = _newUserholiday,
                Status = HolidayAcception.StatusType.Pending,
                Description = "Wish you good health :)",
            };

            #region setup mocks
            _repositoryMock = new Mock<IHolidayRepository>();
            _repositoryMock.Setup(repo => repo.FindAsync(It.IsAny<Guid>())).ReturnsAsync((Holiday)null);
            _repositoryMock.Setup(repo => repo.FindAsync(_registeredholiday.Id)).ReturnsAsync(_registeredholiday);
            _repositoryMock.Setup(repo => repo.FindPersonalBookingsByDate(_registeredUser.Id, It.IsAny<DateTime>()))
                .ReturnsAsync((List<Holiday>)null);
            _repositoryMock.Setup(repo => repo.FindPersonalBookingsByDate(_registeredUser.Id, _registeredholiday.Start))
                .ReturnsAsync(new List<Holiday> { _registeredholiday });
            _repositoryMock.Setup(repo => repo.FindPersonalBooking(_registeredholiday.Id)).ReturnsAsync(_registeredholiday);
            _repositoryMock.Setup(repo => repo.FindAllBookingsByDate(It.IsAny<DateTime>()))
                .ReturnsAsync((List<Holiday>)null);
            _repositoryMock.Setup(repo => repo.FindAllBookingsByDate(_registeredholiday.Start))
                .ReturnsAsync(new List<Holiday> { _registeredholiday });

            _holidayacceptionRepositoryMock = new Mock<IHolidayAcceptionRepository>();

            _holidayacceptionRepositoryMock.Setup(repo => repo.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync((HolidayAcception)null);
            _holidayacceptionRepositoryMock.Setup(repo => repo.FindAsync(_registeredholidayacception.Id))
                .ReturnsAsync(_registeredholidayacception);
            _holidayacceptionRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<HolidayAcception>())).
                Returns(Task.FromResult(Guid.Empty));

            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock.Setup(uow => uow.Holidays).Returns(_repositoryMock.Object);
            _unitOfWorkMock.Setup(uow => uow.HolidaysAcception).Returns(_holidayacceptionRepositoryMock.Object);
            _unitOfWorkMock.Setup(uow => uow.CommitAsync());
            #endregion

            _service = new BookingService(_mapper, _unitOfWorkMock.Object);
        }

        /// <summary>
        /// Test new booking addition.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task Add_NewBooking()
        {
            var bookingDto = _mapper.Map<Core.DTO.Holiday>(_newUserholiday);

            await _service.Add(bookingDto);

            _repositoryMock.Verify(repo => repo.AddAsync(It.Is<Holiday>(holiday =>
                holiday.Id == _newUserholiday.Id &&
                holiday.UserId == _newUserholiday.UserId &&
                holiday.Category == _newUserholiday.Category &&
                holiday.Hours == _newUserholiday.Hours &&
                holiday.Start == _newUserholiday.Start &&
                holiday.Expires == _newUserholiday.Expires &&
                holiday.Description == _newUserholiday.Description)));

            _unitOfWorkMock.Verify(ouw => ouw.CommitAsync());
        }

        /// <summary>
        /// Test new booking addition with wrong hours of holiday.
        /// </summary>
        [Test]
        public void Add_NewBooking_ArgumentException()
        {
            var bookingDto = _mapper.Map<Core.DTO.Holiday>(_newUserholiday);

            bookingDto.Start = DateTime.Today;
            bookingDto.Expires = DateTime.Today.AddDays(-1);

            Assert.That(
                () => _service.Add(bookingDto),
                Throws.ArgumentException.With.Message.EqualTo($"Терміни відпустки неправильно вказані"));
        }

        /// <summary>
        /// Test new booking approval.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task Add_NewBooking_Approval()
        {
            var bookingDto = _mapper.Map<Core.DTO.HolidayAcception>(_newUserholidayacception);

            await _service.Approve(bookingDto);

            _holidayacceptionRepositoryMock.Verify(repo => repo.AddAsync(It.Is<HolidayAcception>(holidayAcc =>
                holidayAcc.Id == _newUserholidayacception.Id &&
                holidayAcc.HolidayId == _newUserholidayacception.HolidayId &&
                holidayAcc.Status == _newUserholidayacception.Status &&
                holidayAcc.Description == _newUserholidayacception.Description)));

            _unitOfWorkMock.Verify(ouw => ouw.CommitAsync());
        }

        /// <summary>
        /// Test asynchronous removing holiday.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task Remove_RegisteredHoliday()
        {
            var holidayDto = _mapper.Map<Core.DTO.Holiday>(_registeredholiday);

            await _service.Remove(holidayDto.Id);

            _repositoryMock.Verify(repo => repo.Remove(It.Is<Holiday>(holiday =>
                holiday.Id == _registeredholiday.Id &&
                holiday.UserId == _registeredholiday.UserId &&
                holiday.Category == _registeredholiday.Category &&
                holiday.Hours == _registeredholiday.Hours &&
                holiday.Start == _registeredholiday.Start &&
                holiday.Expires == _registeredholiday.Expires &&
                holiday.Description == _registeredholiday.Description)));

            _unitOfWorkMock.Verify(ouw => ouw.CommitAsync());
        }

        /// <summary>
        /// Test asynchronous removing non-existent holiday in db.
        /// </summary>
        [Test]
        public void Remove_NotRegisteredHoliday_ArgumentException()
        {
            Assert.That(
                () => _service.Remove(_newUserholiday.Id),
                Throws.ArgumentException.With.Message
                .EqualTo($"Неможливо знайти відпустку з id (Guid){_newUserholiday.Id}"));
        }

        /// <summary>
        /// Test personal booking finding in db.
        /// </summary>
        /// <returns>Holiday Dto.</returns>
        [Test]
        public async Task Find_PersonalBooking_ByHolidayId()
        {
            var holidayDto = await _service.FindPersonalBooking(_registeredholiday.Id);

            Assert.AreEqual(holidayDto.Id, _registeredholiday.Id);

            Assert.AreEqual(holidayDto.UserId, _registeredholiday.UserId);

            Assert.AreEqual(holidayDto.Category, _registeredholiday.Category.ToString());

            Assert.AreEqual(holidayDto.Hours, _registeredholiday.Hours);

            Assert.AreEqual(holidayDto.Start, _registeredholiday.Start);

            Assert.AreEqual(holidayDto.Expires, _registeredholiday.Expires);

            Assert.AreEqual(holidayDto.Description, _registeredholiday.Description);
        }

        /// <summary>
        /// Test all holiday bookings finding by date in db.
        /// </summary>
        /// <returns>Holiday Dtos.</returns>
        [Test]
        public async Task Find_AllBookings_ByDate()
        {
            var holidayDtos = await _service.FindAllBookingsByDate(_registeredholiday.Start);

            Assert.That(
                holidayDtos.First().Id,
                Is.EqualTo(_registeredholiday.Id));

            Assert.That(
                holidayDtos.First().Start,
                Is.EqualTo(_registeredholiday.Start));

            Assert.AreEqual(holidayDtos.Count(), 1);
        }

        /// <summary>
        /// Test personal holiday bookings finding by user id and date in db.
        /// </summary>
        /// <returns>Holiday Dtos.</returns>
        [Test]
        public async Task Find_PersonalBookings_ByDate()
        {
            var holidayDtos = await _service.FindPersonalBookingsByDate(
                _registeredholiday.UserId,
                _registeredholiday.Start);

            Assert.That(
                holidayDtos.First().Id,
                Is.EqualTo(_registeredholiday.Id));

            Assert.That(
                holidayDtos.First().Start,
                Is.EqualTo(_registeredholiday.Start));

            Assert.AreEqual(holidayDtos.Count(), 1);
        }
    }
}
