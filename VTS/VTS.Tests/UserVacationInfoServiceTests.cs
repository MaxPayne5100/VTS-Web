using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using NUnit.Framework;
using VTS.Core.Constants;
using VTS.DAL.Entities;
using VTS.Repos.UnitOfWork;
using VTS.Repos.UsersVacationInfo;
using VTS.Services;
using VTS.Services.UserVacationInfoService;

namespace VTS.Tests
{
    /// <summary>
    /// Tests for user vacation info logic service.
    /// </summary>
    public class UserVacationInfoServiceTests
    {
        private IUserVacationInfoService _service;

        private IMapper _mapper;

        private Mock<IUserVacationInfoRepository> _repositoryMock;

        private Mock<IUnitOfWork> _unitOfWorkMock;

        private User _registeredUser;
        private User _newUser;
        private UserVacationInfo _registeredUserVacationInfo;
        private UserVacationInfo _newUserVacationInfo;

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

            _registeredUserVacationInfo = new UserVacationInfo()
            {
                Id = Guid.NewGuid(),
                UserId = _registeredUser.Id,
                User = _registeredUser,
                PaidDayOffs = 15,
                UnPaidDayOffs = 20,
                PaidSickness = 10,
                UnPaidSickness = 15,
                BonusPaidDayOffs = 0,
                StartedWorking = new DateTime(2019, 7, 20),
            };

            _newUserVacationInfo = new UserVacationInfo()
            {
                Id = Guid.NewGuid(),
                UserId = _newUser.Id,
                User = _newUser,
                PaidDayOffs = 13,
                UnPaidDayOffs = 21,
                PaidSickness = 9,
                UnPaidSickness = 13,
                BonusPaidDayOffs = 0,
                StartedWorking = new DateTime(2019, 6, 15),
            };

            #region setup mocks
            _repositoryMock = new Mock<IUserVacationInfoRepository>();
            _repositoryMock.Setup(repo => repo.FindAsync(It.IsAny<Guid>())).ReturnsAsync((UserVacationInfo)null);
            _repositoryMock.Setup(repo => repo.FindAsync(_registeredUserVacationInfo.Id)).ReturnsAsync(_registeredUserVacationInfo);
            _repositoryMock.Setup(repo => repo.FindByUserId(It.IsAny<int>())).ReturnsAsync((UserVacationInfo)null);
            _repositoryMock.Setup(repo => repo.FindByUserId(_registeredUser.Id)).ReturnsAsync(_registeredUserVacationInfo);

            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock.Setup(uow => uow.UsersVacationInfo).Returns(_repositoryMock.Object);
            _unitOfWorkMock.Setup(uow => uow.CommitAsync());
            #endregion

            _service = new UserVacationInfoService(_mapper, _unitOfWorkMock.Object);
        }

        /// <summary>
        /// Test user vacation info finding in db.
        /// </summary>
        /// <returns>User vacation info Dto.</returns>
        [Test]
        public async Task Find_UserVacationInfo_ByUserId()
        {
            var usersVacationInfoDto = await _service.FindByUserId(_registeredUser.Id);

            Assert.AreEqual(usersVacationInfoDto.Id, _registeredUserVacationInfo.Id);

            Assert.AreEqual(usersVacationInfoDto.UserId, _registeredUserVacationInfo.UserId);

            Assert.AreEqual(usersVacationInfoDto.PaidDayOffs, _registeredUserVacationInfo.PaidDayOffs);

            Assert.AreEqual(usersVacationInfoDto.UnPaidDayOffs, _registeredUserVacationInfo.UnPaidDayOffs);

            Assert.AreEqual(usersVacationInfoDto.PaidSickness, _registeredUserVacationInfo.PaidSickness);

            Assert.AreEqual(usersVacationInfoDto.UnPaidSickness, _registeredUserVacationInfo.UnPaidSickness);

            Assert.AreEqual(usersVacationInfoDto.BonusPaidDayOffs, _registeredUserVacationInfo.BonusPaidDayOffs);
        }

        /// <summary>
        /// Test asynchronous updatе user's bonus paid day offs.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task Update_BonusDayOffs()
        {
            var userVacationInfoDto = _mapper.Map<Core.DTO.UserVacationInfo>(_registeredUserVacationInfo);

            var updatedData = new Dictionary<string, uint>
            {
                ["BonusPaidDayOffs"] = 5,
            };

            userVacationInfoDto.BonusPaidDayOffs = updatedData["BonusPaidDayOffs"];

            await _service.UpdateBonusDayOffs(userVacationInfoDto);

            _repositoryMock.Verify(repo => repo.Update(It.Is<UserVacationInfo>(userVacationInfo =>
                userVacationInfo.Id == _registeredUserVacationInfo.Id &&
                userVacationInfo.BonusPaidDayOffs == updatedData["BonusPaidDayOffs"])));

            _unitOfWorkMock.Verify(ouw => ouw.CommitAsync());
        }

        /// <summary>
        /// Test asynchronous update user's bonus paid day offs from non-existent user in db.
        /// </summary>
        [Test]
        public void Update_BonusDayOffs_NotRegisteredUser_ArgumentException()
        {
            var userVacationInfoDto = _mapper.Map<Core.DTO.UserVacationInfo>(_newUserVacationInfo);

            Assert.That(
                () => _service.UpdateBonusDayOffs(userVacationInfoDto),
                Throws.ArgumentException.With.Message.EqualTo($"Неможливо знайти користувача з id {userVacationInfoDto.UserId}"));
        }

        /// <summary>
        /// Test asynchronous update user's vacation info.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task Update_VacationInfo()
        {
            var userVacationInfoDto = _mapper.Map<Core.DTO.UserVacationInfo>(_registeredUserVacationInfo);

            var updatedData = new Dictionary<string, uint>
            {
                ["PaidDayOffs"] = 13,
                ["UnPaidDayOffs"] = 21,
                ["PaidSickness"] = 9,
                ["UnPaidSickness"] = 13,
                ["BonusPaidDayOffs"] = 3,
            };

            userVacationInfoDto.PaidDayOffs = updatedData["PaidDayOffs"];
            userVacationInfoDto.UnPaidDayOffs = updatedData["UnPaidDayOffs"];
            userVacationInfoDto.PaidSickness = updatedData["PaidSickness"];
            userVacationInfoDto.UnPaidSickness = updatedData["UnPaidSickness"];
            userVacationInfoDto.BonusPaidDayOffs = updatedData["BonusPaidDayOffs"];
            userVacationInfoDto.StartedWorking = _registeredUserVacationInfo.StartedWorking;

            await _service.UpdateVacationInfo(userVacationInfoDto);

            _repositoryMock.Verify(repo => repo.Update(It.Is<UserVacationInfo>(userVacationInfo =>
                userVacationInfo.Id == _registeredUserVacationInfo.Id &&
                userVacationInfo.PaidDayOffs == updatedData["PaidDayOffs"] &&
                userVacationInfo.UnPaidDayOffs == updatedData["UnPaidDayOffs"] &&
                userVacationInfo.PaidSickness == updatedData["PaidSickness"] &&
                userVacationInfo.UnPaidSickness == updatedData["UnPaidSickness"] &&
                userVacationInfo.BonusPaidDayOffs == updatedData["BonusPaidDayOffs"] &&
                userVacationInfo.StartedWorking == _registeredUserVacationInfo.StartedWorking)));

            _unitOfWorkMock.Verify(ouw => ouw.CommitAsync());
        }

        /// <summary>
        /// Test asynchronous update vacation info from non-existent user in db.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task Update_VacationInfo_NotRegisteredUser()
        {
            var userVacationInfoDto = _mapper.Map<Core.DTO.UserVacationInfo>(_newUserVacationInfo);

            await _service.UpdateVacationInfo(userVacationInfoDto);

            _repositoryMock.Verify(repo => repo.AddAsync(It.Is<UserVacationInfo>(userVacationInfo =>
                userVacationInfo.Id == _newUserVacationInfo.Id &&
                userVacationInfo.PaidDayOffs == _newUserVacationInfo.PaidDayOffs &&
                userVacationInfo.UnPaidDayOffs == _newUserVacationInfo.UnPaidDayOffs &&
                userVacationInfo.PaidSickness == _newUserVacationInfo.PaidSickness &&
                userVacationInfo.UnPaidSickness == _newUserVacationInfo.UnPaidSickness &&
                userVacationInfo.BonusPaidDayOffs == _newUserVacationInfo.BonusPaidDayOffs &&
                userVacationInfo.StartedWorking == _newUserVacationInfo.StartedWorking)));

            _unitOfWorkMock.Verify(ouw => ouw.CommitAsync());
        }

        /// <summary>
        /// Test asynchronous update user's vacation info after booking.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task Update_VacationInfo_AfterBooking()
        {
            var userVacationInfoDto = _mapper.Map<Core.DTO.UserVacationInfo>(_registeredUserVacationInfo);
            uint days = 7;
            string category = VacationCategories.PaidDayOffs;
            DateTime start = DateTime.Today.AddDays(5);

            await _service.AfterBookingUpdate(days, category, start, userVacationInfoDto);

            _repositoryMock.Verify(repo => repo.Update(It.Is<UserVacationInfo>(userVacationInfo =>
                userVacationInfo.Id == _registeredUserVacationInfo.Id &&
                userVacationInfo.PaidDayOffs == _registeredUserVacationInfo.PaidDayOffs)));

            _unitOfWorkMock.Verify(ouw => ouw.CommitAsync());
        }

        /// <summary>
        /// Test asynchronous update user's vacation info after booking with exceptional cases.
        /// </summary>
        [Test]
        public void Update_VacationInfo_AfterBooking_ArgumentException()
        {
            var userVacationInfoDto = _mapper.Map<Core.DTO.UserVacationInfo>(_newUserVacationInfo);
            uint days = 7;
            string category = VacationCategories.UnPaidSickness;
            DateTime start = DateTime.Today.AddDays(5);

            Assert.That(
                () => _service.AfterBookingUpdate(days, category, start, userVacationInfoDto),
                Throws.ArgumentException.With.Message.EqualTo("Не знайдено ваші дані про відпуск"));

            userVacationInfoDto = _mapper.Map<Core.DTO.UserVacationInfo>(_registeredUserVacationInfo);
            days = 16;

            Assert.That(
                () => _service.AfterBookingUpdate(days, category, start, userVacationInfoDto),
                Throws.ArgumentException.With.Message.EqualTo("Забагато днів лікарняного"));

            userVacationInfoDto = _mapper.Map<Core.DTO.UserVacationInfo>(_registeredUserVacationInfo);
            days = 7;
            userVacationInfoDto.StartedWorking = DateTime.Today.AddDays(-5);

            Assert.That(
                () => _service.AfterBookingUpdate(days, category, start, userVacationInfoDto),
                Throws.ArgumentException.With.Message.EqualTo("У вас замало досвіду в компанії щоб замовити відпустку"));
        }

        /// <summary>
        /// Test asynchronous update user's vacation info after delete booking.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task Update_VacationInfo_AfterDeleteBooking()
        {
            var userVacationInfoDto = _mapper.Map<Core.DTO.UserVacationInfo>(_registeredUserVacationInfo);
            uint days = 7;
            string category = VacationCategories.UnPaidDayOffs;

            await _service.AfterDeleteBookingUpdate(days, category, userVacationInfoDto);

            _repositoryMock.Verify(repo => repo.Update(It.Is<UserVacationInfo>(userVacationInfo =>
                userVacationInfo.Id == _registeredUserVacationInfo.Id &&
                userVacationInfo.UnPaidDayOffs == _registeredUserVacationInfo.UnPaidDayOffs)));

            _unitOfWorkMock.Verify(ouw => ouw.CommitAsync());
        }

        /// <summary>
        /// Test asynchronous update vacation info after delete booking from non-existent user in db.
        /// </summary>
        [Test]
        public void Update_VacationInfo_AfterDeleteBooking_ArgumentException()
        {
            var userVacationInfoDto = _mapper.Map<Core.DTO.UserVacationInfo>(_newUserVacationInfo);
            uint days = 7;
            string category = VacationCategories.PaidSickness;

            Assert.That(
                () => _service.AfterDeleteBookingUpdate(days, category, userVacationInfoDto),
                Throws.ArgumentException.With.Message.EqualTo("Не знайдено ваші дані про відпуск"));
        }
    }
}
