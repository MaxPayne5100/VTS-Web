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
using VTS.Repos.Users;
using VTS.Services;
using VTS.Services.UserService;

namespace VTS.Tests
{
    /// <summary>
    /// Tests for user logic service.
    /// </summary>
    public class UserServiceTests
    {
        private IUserService _service;

        private IMapper _mapper;

        private Mock<IUserRepository> _repositoryMock;
        private Mock<IEmployeeRepository> _employeeRepositoryMock;

        private Mock<IUnitOfWork> _unitOfWorkMock;

        private User _registeredUser;
        private User _registeredUser2;
        private User _newUser;
        private Employee _registeredEmployee;

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

            _registeredUser2 = new User()
            {
                Id = 1234,
                FirstName = "Firstname2",
                LastName = "Lastname2",
                Email = "registered2@gmail.com",
                Role = Roles.Employee,
                Employee = _registeredEmployee,
            };

            _newUser = new User()
            {
                Id = 111,
                FirstName = "Firstname",
                LastName = "Lastname",
                Email = "new@gmail.com",
            };

            _registeredEmployee = new Employee()
            {
                Id = 1,
                UserId = _registeredUser2.Id,
                User = _registeredUser2,
            };

            #region setup mocks
            _repositoryMock = new Mock<IUserRepository>();
            _repositoryMock.Setup(repo => repo.FindByRoleWithoutOwnData(It.IsAny<string>(), _registeredUser.Id)).ReturnsAsync((List<User>)null);
            _repositoryMock.Setup(repo => repo.FindByRoleWithoutOwnData(It.IsAny<string>(), _registeredUser.Id)).ReturnsAsync(new List<User> { _registeredUser, _registeredUser2 });
            _repositoryMock.Setup(repo => repo.FindByEmail(It.IsAny<string>())).ReturnsAsync((User)null);
            _repositoryMock.Setup(repo => repo.FindByEmail(_registeredUser.Email)).ReturnsAsync(_registeredUser);
            _repositoryMock.Setup(repo => repo.FindByEmail(_registeredUser2.Email)).ReturnsAsync(_registeredUser2);
            _repositoryMock.Setup(repo => repo.FindAsync(It.IsAny<int>())).ReturnsAsync((User)null);
            _repositoryMock.Setup(repo => repo.FindAsync(_registeredUser.Id)).ReturnsAsync(_registeredUser);
            _repositoryMock.Setup(repo => repo.FindAsync(_registeredUser2.Id)).ReturnsAsync(_registeredUser2);
            _repositoryMock.Setup(repo => repo.FindWithAllRolesInfoById(It.IsAny<int>())).ReturnsAsync((User)null);
            _repositoryMock.Setup(repo => repo.FindWithAllRolesInfoById(_registeredUser.Id)).ReturnsAsync(_registeredUser);
            _repositoryMock.Setup(repo => repo.FindWithAllRolesInfoById(_registeredUser2.Id)).ReturnsAsync(_registeredUser2);

            _repositoryMock.Setup(repo => repo.AddAsync(It.IsAny<User>())).Returns(Task.FromResult(0));
            _repositoryMock.Setup(repo => repo.Update(_registeredUser));
            _repositoryMock.Setup(repo => repo.Remove(_registeredUser));
            _repositoryMock.Setup(repo => repo.Update(_registeredUser2));
            _repositoryMock.Setup(repo => repo.Remove(_registeredUser2));

            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _employeeRepositoryMock.Setup(repo => repo.FindAsync(It.IsAny<int>())).ReturnsAsync((Employee)null);
            _employeeRepositoryMock.Setup(repo => repo.FindAsync(_registeredEmployee.Id)).ReturnsAsync(_registeredEmployee);
            _employeeRepositoryMock.Setup(repo => repo.FindEmployeeByUserId(It.IsAny<int>())).ReturnsAsync((Employee)null);
            _employeeRepositoryMock.Setup(repo => repo.FindEmployeeByUserId(_registeredUser2.Id)).ReturnsAsync(_registeredEmployee);
            _employeeRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Employee>())).Returns(Task.FromResult(0));

            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock.Setup(uow => uow.Users).Returns(_repositoryMock.Object);
            _unitOfWorkMock.Setup(uow => uow.Employees).Returns(_employeeRepositoryMock.Object);
            _unitOfWorkMock.Setup(uow => uow.CommitAsync());
            #endregion

            _service = new UserService(_mapper, _unitOfWorkMock.Object);
        }

        /// <summary>
        /// Test new user addition.
        /// </summary>
        /// <returns>User claims.</returns>
        [Test]
        public async Task Add_NewUser()
        {
            var userDto = _mapper.Map<Core.DTO.User>(_newUser);

            await _service.Add(userDto);

            _repositoryMock.Verify(repo => repo.AddAsync(It.Is<User>(user =>
                user.Id == _newUser.Id &&
                user.FirstName == _newUser.FirstName &&
                user.LastName == _newUser.LastName &&
                user.Email == _newUser.Email)));

            _unitOfWorkMock.Verify(ouw => ouw.CommitAsync());
        }

        /// <summary>
        /// Test already added in db user.
        /// </summary>
        [Test]
        public void Add_RegisteredUser_ArgumentException()
        {
            var userDto = _mapper.Map<Core.DTO.User>(_registeredUser);

            Assert.That(
                () => _service.Add(userDto),
                Throws.ArgumentException.With.Message.EqualTo($"Користувач з email {userDto.Email} вже існує"));
        }

        /// <summary>
        /// Test user finding in db.
        /// </summary>
        /// <returns>User claims.</returns>
        [Test]
        public async Task Find_RegisteredUser()
        {
            var userDto = await _service.Find(_registeredUser.Id);

            Assert.AreEqual(userDto.Id, _registeredUser.Id);

            Assert.AreEqual(userDto.FirstName, _registeredUser.FirstName);

            Assert.AreEqual(userDto.LastName, _registeredUser.LastName);

            Assert.AreEqual(userDto.Role, _registeredUser.Role);

            Assert.AreEqual(userDto.Email, _registeredUser.Email);
        }

        /// <summary>
        /// Test non-existent user finding in db.
        /// </summary>
        [Test]
        public void Find_NotRegisteredUser_ArgumentException()
        {
            Assert.That(
                () => _service.Find(_newUser.Id),
                Throws.ArgumentException.With.Message.EqualTo($"Неможливо найти користувача з id {_newUser.Id}"));
        }

        /// <summary>
        /// Test finding users by role without own data.
        /// </summary>
        /// <returns>List of users.</returns>
        [Test]
        public async Task Find_Users_ByRole()
        {
            _registeredUser.Role = Roles.Employee;

            var usersDto = await _service.FindByRoleWithoutOwnData(_registeredUser.Role, _registeredUser.Id);

            Assert.That(
                usersDto.First().Role,
                Is.EqualTo(_registeredUser.Role));

            Assert.That(
                usersDto.First().Id,
                Is.EqualTo(_registeredUser.Id));

            Assert.That(
                usersDto.Last().Id,
                Is.EqualTo(_registeredUser2.Id));

            Assert.AreEqual(usersDto.Count(), 2);
        }

        /// <summary>
        /// Test finding users by null role without own data.
        /// </summary>
        /// <returns>List of users.</returns>
        [Test]
        public async Task Find_Users_ByNullRole()
        {
            var usersDto = await _service.FindByRoleWithoutOwnData(_registeredUser.Role, _registeredUser.Id);

            Assert.That(usersDto, Is.Empty);
        }

        /// <summary>
        /// Test asynchronous removing user.
        /// </summary>
         /// <returns>Task.</returns>
        [Test]
        public async Task Remove_RegisteredUser()
        {
            var userDto = _mapper.Map<Core.DTO.User>(_registeredUser);

            await _service.Remove(_registeredUser.Id);

            _repositoryMock.Verify(repo => repo.Remove(It.Is<User>(user =>
                user.Id == _registeredUser.Id &&
                user.FirstName == _registeredUser.FirstName &&
                user.LastName == _registeredUser.LastName &&
                user.Email == _registeredUser.Email)));

            _unitOfWorkMock.Verify(ouw => ouw.CommitAsync());
        }

        /// <summary>
        /// Test asynchronous removing non-existent user in db.
        /// </summary>
        [Test]
        public void Remove_NotRegisteredUser_ArgumentException()
        {
            Assert.That(
                () => _service.Remove(_newUser.Id),
                Throws.ArgumentException.With.Message.EqualTo($"Неможливо найти користувача з id {_newUser.Id}"));
        }

        /// <summary>
        /// Test asynchronous update user's email, firstname and lastname.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task Update_Profile_ByEmail()
        {
            var userDto = _mapper.Map<Core.DTO.User>(_registeredUser);

            var updatedData = new Dictionary<string, string>
            {
                ["Firstname"] = "updated_Firstname",
                ["Lastname"] = "updated_Lastname",
                ["Email"] = "updated_registered@gmail.com",
            };

            userDto.FirstName = updatedData["Firstname"];
            userDto.LastName = updatedData["Lastname"];
            userDto.Email = updatedData["Email"];

            await _service.UpdateProfile(userDto);

            _repositoryMock.Verify(repo => repo.Update(It.Is<User>(user =>
                user.Id == _registeredUser.Id &&
                user.FirstName == updatedData["Firstname"] &&
                user.LastName == updatedData["Lastname"] &&
                user.Email == updatedData["Email"])));

            _unitOfWorkMock.Verify(ouw => ouw.CommitAsync());
        }

        /// <summary>
        /// Test asynchronous update user's email that is already in db.
        /// </summary>
        [Test]
        public void Update_Profile_ByAlreadyTakenEmail_ArgumentException()
        {
            var userDto = _mapper.Map<Core.DTO.User>(_registeredUser);

            userDto.Email = _registeredUser2.Email;

            Assert.That(
                () => _service.UpdateProfile(userDto),
                Throws.ArgumentException.With.Message.EqualTo($"Користувач з email {userDto.Email} вже існує"));
        }

        /// <summary>
        /// Test asynchronous update properties from non-existent user in db.
        /// </summary>
        [Test]
        public void Update_NotRegisteredUser_ArgumentException()
        {
            var userDto = _mapper.Map<Core.DTO.User>(_newUser);

            Assert.That(
                () => _service.Update(userDto),
                Throws.ArgumentException.With.Message.EqualTo($"Неможливо найти користувача з id {_newUser.Id}"));
        }

        /// <summary>
        /// Test asynchronous update user's email that is already in db.
        /// </summary>
        [Test]
        public void Update_ByAlreadyTakenEmail_ArgumentException()
        {
            var userDto = _mapper.Map<Core.DTO.User>(_registeredUser2);

            userDto.Email = _registeredUser.Email;

            Assert.That(
                () => _service.Update(userDto),
                Throws.ArgumentException.With.Message.EqualTo($"Користувач з email {userDto.Email} вже існує"));
        }

        /// <summary>
        /// Test asynchronous update all user's properties.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task Update_RegisteredUser()
        {
            var userDto = _mapper.Map<Core.DTO.User>(_registeredUser);

            var updatedData = new Dictionary<string, string>
            {
                ["Firstname"] = "updated_Firstname",
                ["Lastname"] = "updated_Lastname",
                ["Email"] = "updated_registered@gmail.com",
                ["Role"] = Roles.Employee,
            };

            userDto.FirstName = updatedData["Firstname"];
            userDto.LastName = updatedData["Lastname"];
            userDto.Email = updatedData["Email"];
            userDto.Role = updatedData["Role"];

            await _service.Update(userDto);

            _repositoryMock.Verify(repo => repo.Update(It.Is<User>(user =>
                user.Id == _registeredUser.Id &&
                user.FirstName == updatedData["Firstname"] &&
                user.LastName == updatedData["Lastname"] &&
                user.Email == updatedData["Email"] &&
                user.Role == updatedData["Role"])));

            _unitOfWorkMock.Verify(ouw => ouw.CommitAsync());
        }

        /// <summary>
        /// Test finding user with manager info by id.
        /// </summary>
        /// <returns>User.</returns>
        [Test]
        public async Task Find_ById_WithManagerInfo()
        {
            var userDto = await _service.FindWithManagerInfoById(_registeredUser.Id);

            Assert.AreEqual(userDto.Id, _registeredUser.Id);

            Assert.AreEqual(userDto.FirstName, _registeredUser.FirstName);

            Assert.AreEqual(userDto.LastName, _registeredUser.LastName);

            Assert.AreEqual(userDto.Email, _registeredUser.Email);

            Assert.AreEqual(userDto.Role, _registeredUser.Role);
        }
    }
}