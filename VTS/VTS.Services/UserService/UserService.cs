using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using VTS.Core.Constants;
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
        private async Task<DAL.Entities.User> FindUserEntity(int id)
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

                userDto.Id = user.Id;

                if (user.Role == Roles.Clerk || user.Role == Roles.Manager)
                {
                    var head = new DAL.Entities.Head()
                    {
                        UserId = user.Id,
                    };

                    await _unitOfWork.Heads.AddAsync(head);
                    await _unitOfWork.CommitAsync();

                    if (user.Role == Roles.Clerk)
                    {
                        var clerk = new DAL.Entities.Clerk()
                        {
                            HeadId = head.Id,
                        };

                        await _unitOfWork.Clerks.AddAsync(clerk);
                        await _unitOfWork.CommitAsync();
                    }
                    else
                    {
                        var manager = new DAL.Entities.Manager()
                        {
                            HeadId = head.Id,
                        };

                        await _unitOfWork.Managers.AddAsync(manager);
                        await _unitOfWork.CommitAsync();
                    }
                }
                else if (user.Role == Roles.Employee)
                {
                    var employee = new DAL.Entities.Employee()
                    {
                        UserId = user.Id,
                        ManagerId = userDto.ManagerId,
                    };

                    await _unitOfWork.Employees.AddAsync(employee);
                    await _unitOfWork.CommitAsync();
                }
            }
            else
            {
                throw new ArgumentException($"User with email {userDto.Email} already exists");
            }
        }

        /// <inheritdoc />
        public async Task<Core.DTO.User> Find(int id)
        {
            var user = await FindUserEntity(id);
            var userDto = _mapper.Map<Core.DTO.User>(user);
            return userDto;
        }

        /// <inheritdoc />
        public async Task Remove(int id)
        {
            var user = await FindUserEntity(id);
            _unitOfWork.Users.Remove(user);
            await _unitOfWork.CommitAsync();
        }

        /// <inheritdoc/>
        public async Task Update(Core.DTO.User userDto)
        {
            var user = await _unitOfWork.Users.FindWithAllRolesInfoById(userDto.Id);

            if (user == null)
            {
                throw new ArgumentException($"Can not find user with id {userDto.Id}");
            }

            if (user.Email == userDto.Email || await CheckIfEmailAllowed(userDto.Email))
            {
                user.Email = userDto.Email;
                user.FirstName = userDto.FirstName;
                user.LastName = userDto.LastName;

                if (user.Role == Roles.Clerk || user.Role == Roles.Manager)
                {
                    _unitOfWork.Heads.Remove(user.Head);
                    await _unitOfWork.CommitAsync();
                }
                else if (user.Role == Roles.Employee)
                {
                    _unitOfWork.Employees.Remove(user.Employee);
                    await _unitOfWork.CommitAsync();
                }

                if (userDto.Role == Roles.Clerk || userDto.Role == Roles.Manager)
                {
                    var head = new DAL.Entities.Head()
                    {
                        UserId = user.Id,
                    };

                    await _unitOfWork.Heads.AddAsync(head);
                    await _unitOfWork.CommitAsync();

                    if (userDto.Role == Roles.Clerk)
                    {
                        var clerk = new DAL.Entities.Clerk()
                        {
                            HeadId = head.Id,
                        };

                        await _unitOfWork.Clerks.AddAsync(clerk);
                        await _unitOfWork.CommitAsync();
                    }
                    else
                    {
                        var manager = new DAL.Entities.Manager()
                        {
                            HeadId = head.Id,
                        };

                        await _unitOfWork.Managers.AddAsync(manager);
                        await _unitOfWork.CommitAsync();
                    }
                }
                else if (userDto.Role == Roles.Employee)
                {
                    var employee = new DAL.Entities.Employee()
                    {
                        UserId = user.Id,
                        ManagerId = userDto.ManagerId,
                    };

                    await _unitOfWork.Employees.AddAsync(employee);
                    await _unitOfWork.CommitAsync();
                }

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

        /// <inheritdoc/>
        public async Task<IEnumerable<Core.DTO.User>> FindByRoleWithoutOwnData(string role, int id)
        {
            if (role != null)
            {
                var users = await _unitOfWork.Users.FindByRoleWithoutOwnData(role, id);
                var usersDto = _mapper.Map<IEnumerable<Core.DTO.User>>(users);
                return usersDto;
            }
            else
            {
                return new List<Core.DTO.User>();
            }
        }

        /// <inheritdoc/>
        public async Task<Core.DTO.User> FindWithManagerInfoById(int id)
        {
            var user = await _unitOfWork.Users.FindWithAllRolesInfoById(id);
            var userDto = _mapper.Map<Core.DTO.User>(user);

            if (userDto.Role == Roles.Employee)
            {
                userDto.ManagerId = user.Employee.ManagerId;
            }

            return userDto;
        }

        /// <inheritdoc/>
        public async Task<Core.DTO.Head> FindWithHeadInfoById(int id)
        {
            var head = await _unitOfWork.Heads.FindHeadByUserId(id);
            var headDto = _mapper.Map<Core.DTO.Head>(head);
            return headDto;
        }
    }
}
