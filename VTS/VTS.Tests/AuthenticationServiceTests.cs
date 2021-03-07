using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using VTS.Core.Constants;
using VTS.DAL.Entities;
using VTS.Repos.UnitOfWork;
using VTS.Repos.Users;
using VTS.Services.AuthenticationService;

namespace VTS.Tests
{
    /// <summary>
    /// Tests for authentication service.
    /// </summary>
    public class AuthenticationServiceTests
    {
        private IAuthenticationService _service;

        private Mock<IUserRepository> _repositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;

        private User _registeredUser;
        private User _registeredUser2;
        private User _registeredUser3;

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
                Role = Roles.Clerk,
            };

            _registeredUser2 = new User()
            {
                Id = 1234,
                FirstName = "Firstname2",
                LastName = "Lastname2",
                Email = "registered2@gmail.com",
                Role = Roles.Employee,
            };

            _registeredUser3 = new User()
            {
                Id = 1234,
                FirstName = "Firstname",
                LastName = "Lastname",
                Email = "registered@gmail.com",
                Role = Roles.Clerk,
            };

            #region setup mocks
            _repositoryMock = new Mock<IUserRepository>();
            _repositoryMock.Setup(repo => repo.FindByEmail(It.IsAny<string>())).ReturnsAsync((User)null);
            _repositoryMock.Setup(repo => repo.FindByEmail(_registeredUser.Email)).ReturnsAsync(_registeredUser);
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock.Setup(uow => uow.Users).Returns(_repositoryMock.Object);
            _unitOfWorkMock.Setup(uow => uow.CommitAsync());
            #endregion

            _service = new AuthenticationService(_unitOfWorkMock.Object);
        }

        /// <summary>
        /// Test user log in.
        /// </summary>
        /// <returns>User claims.</returns>
        [Test]
        public async Task LogIn_ClaimsPrincipal()
        {
            var result = await _service.LogIn(_registeredUser.Id, _registeredUser.Email);

            Assert.That(
                result.Claims.Where(c => c.Type == ClaimKeys.Id).Select(c => c.Value).SingleOrDefault(),
                Is.EqualTo(_registeredUser.Id.ToString()));

            Assert.That(
                result.Claims.Where(c => c.Type == ClaimKeys.Email).Select(c => c.Value).SingleOrDefault(),
                Is.EqualTo(_registeredUser.Email));

            Assert.That(
                result.Claims.Where(c => c.Type == ClaimKeys.FirstName).Select(c => c.Value).SingleOrDefault(),
                Is.EqualTo(_registeredUser.FirstName));

            Assert.That(
                result.Claims.Where(c => c.Type == ClaimKeys.LastName).Select(c => c.Value).SingleOrDefault(),
                Is.EqualTo(_registeredUser.LastName));

            Assert.That(
                result.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).SingleOrDefault(),
                Is.EqualTo(_registeredUser.Role));

            _repositoryMock.Verify(repo => repo.FindByEmail(_registeredUser.Email));
        }

        /// <summary>
        /// Test non-existent in db user log in.
        /// </summary>
        [Test]
        public void LogIn_NotRegisteredUser_ArgumentException()
        {
            Assert.That(
                () => _service.LogIn(_registeredUser2.Id, _registeredUser2.Email),
                Throws.ArgumentException.With.Message.EqualTo("Неправильний ID або пошта"));

            Assert.AreEqual(_registeredUser.Email, _registeredUser3.Email);
            Assert.AreNotEqual(_registeredUser.Id, _registeredUser3.Id);

            Assert.That(
                () => _service.LogIn(_registeredUser3.Id, _registeredUser3.Email),
                Throws.ArgumentException.With.Message.EqualTo("Неправильний ID або пошта"));
        }
    }
}