using System;
using System.Threading.Tasks;
using AutoMapper;
using VTS.Repos.UnitOfWork;

namespace VTS.Services.UserService
{
    /// <summary>
    /// Service for user logic.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Helper function that checks if user with specified Id exists.
        /// </summary>
        /// <param name="id">User's id.</param>
        /// <returns>User if exists.</returns>
        private async Task<DAL.Entities.User> FindUserEntity(uint id)
        {
            var user = await _unitOfWork.Users.FindAsync(id);

            if (user != null)
            {
                return user;
            }
            else
            {
                throw new ArgumentException($"Can not find user with id {id}");
            }
        }

        /// <summary>
        /// Checks if email is not already taken.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <returns>False if email is already taken, otherwise true.</returns>
        private async Task<bool> CheckIfEmailAllowed(string email)
        {
            return await _unitOfWork.Users.FindByEmail(email) == null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of work.</param>
        public UserService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public async Task Add(Core.DTO.User userDto)
        {
            var user = await _unitOfWork.Users.FindByEmail(userDto.Email);

            if (user == null)
            {
                user = _mapper.Map<DAL.Entities.User>(userDto);
                await _unitOfWork.Users.AddAsync(user);
                await _unitOfWork.CommitAsync();
            }
            else
            {
                throw new ArgumentException($"User with email {userDto.Email} already exists");
            }
        }

        /// <inheritdoc />
        public async Task<Core.DTO.User> Find(uint id)
        {
            var user = await FindUserEntity(id);
            var userDto = _mapper.Map<Core.DTO.User>(user);
            return userDto;
        }

        /// <inheritdoc />
        public async Task Remove(uint id)
        {
            var user = await FindUserEntity(id);
            _unitOfWork.Users.Remove(user);
            await _unitOfWork.CommitAsync();
        }

        /// <inheritdoc/>
        public async Task Update(Core.DTO.User userDto)
        {
            var user = await FindUserEntity(userDto.Id);

            if (user.Email == userDto.Email || await CheckIfEmailAllowed(userDto.Email))
            {
                user.Email = userDto.Email;
                user.FirstName = userDto.FirstName;
                user.LastName = userDto.LastName;
                user.Role = userDto.Role;

                _unitOfWork.Users.Update(user);
                await _unitOfWork.CommitAsync();
            }
            else
            {
                throw new ArgumentException($"User with email {userDto.Email} already exists");
            }
        }

        /// <inheritdoc />
        public async Task UpdateProfile(Core.DTO.User userDto)
        {
            var user = await FindUserEntity(userDto.Id);

            if (user.Email == userDto.Email || await CheckIfEmailAllowed(userDto.Email))
            {
                user.Email = userDto.Email;
                user.FirstName = userDto.FirstName;
                user.LastName = userDto.LastName;

                _unitOfWork.Users.Update(user);
                await _unitOfWork.CommitAsync();
            }
            else
            {
                throw new ArgumentException($"User with email {userDto.Email} already exists");
            }
        }
    }
}
