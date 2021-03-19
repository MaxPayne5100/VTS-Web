using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using NUnit.Framework;
using VTS.Core.Constants;
using VTS.DAL.Entities;
using VTS.Repos.Managers;
using VTS.Repos.UnitOfWork;
using VTS.Services;
using VTS.Services.ManagerService;

namespace VTS.Tests
{
    /// <summary>
    /// Tests for manager logic service.
    /// </summary>
    public class ManagerServiceTests
    {
        private IManagerService _service;

        private IMapper _mapper;

        private Mock<IManagerRepository> _repositoryMock;

        private Mock<IUnitOfWork> _unitOfWorkMock;

        private Manager _registeredManager;
        private Manager _registeredManager2;

        private Head _registeredHead;
        private Head _registeredHead2;

        private User _registeredUser;
        private User _registeredUser2;
        private User _newUser;

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
                Role = Roles.Manager,
                Head = _registeredHead,
            };

            _registeredUser2 = new User()
            {
                Id = 1234,
                FirstName = "Firstname2",
                LastName = "Lastname2",
                Email = "registered2@gmail.com",
                Role = Roles.Manager,
                Head = _registeredHead2,
            };

            _newUser = new User()
            {
                Id = 111,
                FirstName = "Firstname",
                LastName = "Lastname",
                Email = "new@gmail.com",
            };

            _registeredHead = new Head()
            {
                Id = 1,
                UserId = 123,
                User = _registeredUser,
                Manager = _registeredManager,
            };

            _registeredHead2 = new Head()
            {
                Id = 2,
                UserId = 1234,
                User = _registeredUser2,
                Manager = _registeredManager2,
            };

            _registeredManager = new Manager()
            {
                Id = 1,
                HeadId = 1,
                Head = _registeredHead,
            };

            _registeredManager2 = new Manager()
            {
                Id = 2,
                HeadId = 2,
                Head = _registeredHead2,
            };

            #region setup mocks
            _repositoryMock = new Mock<IManagerRepository>();
            _repositoryMock.Setup(repo => repo.FindAsync(It.IsAny<int>())).ReturnsAsync((Manager)null);
            _repositoryMock.Setup(repo => repo.FindAsync(_registeredManager.Id)).ReturnsAsync(_registeredManager);
            _repositoryMock.Setup(repo => repo.FindAsync(_registeredManager2.Id)).ReturnsAsync(_registeredManager2);
            _repositoryMock.Setup(repo => repo.FindManagerByUserId(It.IsAny<int>())).ReturnsAsync((Manager)null);
            _repositoryMock.Setup(repo => repo.FindManagerByUserId(_registeredUser.Id)).ReturnsAsync(_registeredManager);
            _repositoryMock.Setup(repo => repo.FindManagerByUserId(_registeredUser2.Id)).ReturnsAsync(_registeredManager2);

            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock.Setup(uow => uow.Managers).Returns(_repositoryMock.Object);
            _unitOfWorkMock.Setup(uow => uow.CommitAsync());
            #endregion

            _service = new ManagerService(_mapper, _unitOfWorkMock.Object);
        }

        /// <summary>
        /// Test finding manager by user identifier.
        /// </summary>
        /// <returns>Manager.</returns>
        [Test]
        public async Task Find_Manager_ByUserId()
        {
            var managerDto = await _service.FindManagerByUserId(_registeredUser.Id);

            Assert.AreEqual(managerDto.Id, _registeredManager.Id);

            Assert.AreEqual(managerDto.HeadId, _registeredManager.HeadId);
        }

        /// <summary>
        /// Test finding manager by non-existent user identifier.
        /// </summary>
        [Test]
        public void Find_Manager_ByNotRegisteredUserId_ArgumentException()
        {
            Assert.That(
                () => _service.FindManagerByUserId(_newUser.Id),
                Throws.ArgumentException.With.Message.EqualTo($"Неможливо найти менеджера з id користувача {_newUser.Id}"));
        }

        /// <summary>
        /// Test getting all managers.
        /// </summary>
        /// <returns>IEnumerable of managers.</returns>
        [Test]
        public async Task Get_AllManagers_WithUserInfo()
        {
            var managerDtos = await _service.GetAllWithUserInfo();

            Assert.That(managerDtos, Is.Empty);

            _repositoryMock.Setup(repo => repo.GetAllWithUserInfo()).ReturnsAsync(new List<Manager> { _registeredManager, _registeredManager2 });

            managerDtos = await _service.GetAllWithUserInfo();

            Assert.That(
                managerDtos.First().Id,
                Is.EqualTo(_registeredManager.Id));

            Assert.That(
                managerDtos.Last().Id,
                Is.EqualTo(_registeredManager2.Id));

            Assert.AreEqual(managerDtos.Count(), 2);
        }
    }
}