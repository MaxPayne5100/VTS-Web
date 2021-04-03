using System.Threading.Tasks;
using AutoMapper;
using Moq;
using NUnit.Framework;
using VTS.DAL.Entities;
using VTS.Repos.Heads;
using VTS.Repos.UnitOfWork;
using VTS.Services;
using VTS.Services.HeadService;

namespace VTS.Tests
{
    /// <summary>
    /// Tests for head logic service.
    /// </summary>
    public class HeadServiceTests
    {
        private IHeadService _service;

        private IMapper _mapper;

        private Mock<IHeadRepository> _repositoryMock;

        private Mock<IUnitOfWork> _unitOfWorkMock;

        private User _registeredUser;
        private User _newUser;
        private Head _registeredHead;

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

            _registeredHead = new Head()
            {
                Id = 1,
                UserId = _registeredUser.Id,
                User = _registeredUser,
            };

            #region setup mocks
            _repositoryMock = new Mock<IHeadRepository>();
            _repositoryMock.Setup(repo => repo.FindAsync(It.IsAny<int>())).ReturnsAsync((Head)null);
            _repositoryMock.Setup(repo => repo.FindAsync(_registeredHead.Id)).ReturnsAsync(_registeredHead);
            _repositoryMock.Setup(repo => repo.FindHeadByUserId(It.IsAny<int>())).ReturnsAsync((Head)null);
            _repositoryMock.Setup(repo => repo.FindHeadByUserId(_registeredUser.Id)).ReturnsAsync(_registeredHead);

            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock.Setup(uow => uow.Heads).Returns(_repositoryMock.Object);
            _unitOfWorkMock.Setup(uow => uow.CommitAsync());
            #endregion

            _service = new HeadService(_mapper, _unitOfWorkMock.Object);
        }

        /// <summary>
        /// Test head finding by user id in db.
        /// </summary>
        /// <returns>Head Dto.</returns>
        [Test]
        public async Task Find_Head_ByUserId()
        {
            var headDto = await _service.FindHeadByUserId(_registeredUser.Id);

            Assert.AreEqual(headDto.Id, _registeredHead.Id);

            Assert.AreEqual(headDto.UserId, _registeredUser.Id);

            Assert.AreEqual(headDto.User.Email, _registeredUser.Email);

            Assert.AreEqual(headDto.User.FirstName, _registeredUser.FirstName);

            Assert.AreEqual(headDto.User.LastName, _registeredUser.LastName);
        }

        /// <summary>
        /// Test head finding by non-existent user id in db.
        /// </summary>
        /// <returns>Head Dto.</returns>
        [Test]
        public async Task Find_NullHead_ByNotRegisteredUserId()
        {
            var headDto = await _service.FindHeadByUserId(_newUser.Id);

            Assert.Null(headDto);
        }
    }
}
