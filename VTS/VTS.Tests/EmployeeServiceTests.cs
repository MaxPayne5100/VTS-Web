using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using NUnit.Framework;
using VTS.Core.Constants;
using VTS.DAL.Entities;
using VTS.Repos.Employees;
using VTS.Repos.UnitOfWork;
using VTS.Services;
using VTS.Services.EmployeeService;

namespace VTS.Tests
{
    /// <summary>
    /// Tests for employee logic service.
    /// </summary>
    public class EmployeeServiceTests
    {
        private IEmployeeService _service;

        private IMapper _mapper;

        private Mock<IEmployeeRepository> _repositoryMock;

        private Mock<IUnitOfWork> _unitOfWorkMock;

        private Employee _registeredEmployee;
        private Employee _registeredEmployee2;

        private Manager _registeredManager;

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
                Role = Roles.Employee,
                Employee = _registeredEmployee,
            };

            _registeredUser2 = new User()
            {
                Id = 1234,
                FirstName = "Firstname2",
                LastName = "Lastname2",
                Email = "registered2@gmail.com",
                Role = Roles.Employee,
                Employee = _registeredEmployee2,
            };

            _newUser = new User()
            {
                Id = 111,
                FirstName = "Firstname",
                LastName = "Lastname",
                Email = "new@gmail.com",
            };

            _registeredManager = new Manager()
            {
                Id = 1,
                HeadId = 1,
            };

            _registeredEmployee = new Employee()
            {
                Id = 1,
                ManagerId = 1,
                UserId = 123,
                User = _registeredUser,
            };

            _registeredEmployee2 = new Employee()
            {
                Id = 2,
                ManagerId = 1,
                UserId = 1234,
                User = _registeredUser2,
            };

            #region setup mocks
            _repositoryMock = new Mock<IEmployeeRepository>();
            _repositoryMock.Setup(repo => repo.FindAsync(It.IsAny<int>())).ReturnsAsync((Employee)null);
            _repositoryMock.Setup(repo => repo.FindAsync(_registeredEmployee.Id)).ReturnsAsync(_registeredEmployee);
            _repositoryMock.Setup(repo => repo.FindAsync(_registeredEmployee2.Id)).ReturnsAsync(_registeredEmployee2);

            _repositoryMock.Setup(repo => repo.FindEmployeeByUserId(It.IsAny<int>())).ReturnsAsync((Employee)null);
            _repositoryMock.Setup(repo => repo.FindEmployeeByUserId(_registeredUser.Id)).ReturnsAsync(_registeredEmployee);
            _repositoryMock.Setup(repo => repo.FindEmployeeByUserId(_registeredUser2.Id)).ReturnsAsync(_registeredEmployee2);

            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock.Setup(uow => uow.Employees).Returns(_repositoryMock.Object);
            _unitOfWorkMock.Setup(uow => uow.CommitAsync());
            #endregion

            _service = new EmployeeService(_mapper, _unitOfWorkMock.Object);
        }

        /// <summary>
        /// Asynchronous find employee by user's id.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <returns>Employee.</returns>
        [Test]
        public async Task Find_Employee_ByUserId()
        {
            var employeeDto = await _service.FindEmployeeByUserId(_registeredUser.Id);

            Assert.AreEqual(employeeDto.Id, _registeredEmployee.Id);

            Assert.AreEqual(employeeDto.ManagerId, _registeredEmployee.ManagerId);

            Assert.AreEqual(employeeDto.UserId, _registeredEmployee.UserId);
        }

        /// <summary>
        /// Test finding employee by non-existent user identifier.
        /// </summary>
        [Test]
        public void Find_Employee_ByNotRegisteredUserId_ArgumentException()
        {
            Assert.That(
                () => _service.FindEmployeeByUserId(_newUser.Id),
                Throws.ArgumentException.With.Message.EqualTo($"Неможливо найти працівника з id користувача {_newUser.Id}"));
        }

        /// <summary>
        /// Test finding list of employees by manager's id.
        /// </summary>
        /// <returns>IEnumerable of employees.</returns>
        [Test]
        public async Task Find_ByManagerId()
        {
            var employeesDtos = await _service.FindByManagerId(_registeredManager.Id);

            Assert.That(employeesDtos, Is.Empty);

            _repositoryMock.Setup(repo => repo.FindByManagerId(_registeredManager.Id)).ReturnsAsync(
                new List<Employee> { _registeredEmployee, _registeredEmployee2 });

            employeesDtos = await _service.FindByManagerId(_registeredManager.Id);

            Assert.That(
                employeesDtos.First().Id,
                Is.EqualTo(_registeredEmployee.Id));

            Assert.That(
                employeesDtos.Last().Id,
                Is.EqualTo(_registeredEmployee2.Id));

            Assert.AreEqual(employeesDtos.Count(), 2);
        }
    }
}