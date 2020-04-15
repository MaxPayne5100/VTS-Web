using System.Threading.Tasks;

namespace VTS.Services.UserService
{
    /// <summary>
    /// Interface for User service.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Add user.
        /// </summary>
        /// <param name="userDto">User.</param>
        /// <returns>Task.</returns>
        Task Add(Core.DTO.User userDto);

        /// <summary>
        /// Asynchronous find user.
        /// </summary>
        /// <param name="id">User's id.</param>
        /// <returns>User.</returns>
        Task<Core.DTO.User> Find(uint id);

        /// <summary>
        /// Asynchronous update all user's properties.
        /// </summary>
        /// <param name="userDto">Updated user data.</param>
        /// <returns>Task.</returns>
        Task Update(Core.DTO.User userDto);

        /// <summary>
        /// Asynchronous update user's email, firstname and lastname.
        /// </summary>
        /// <param name="userDto">Updated user data.</param>
        /// <returns>Task.</returns>
        Task UpdateProfile(Core.DTO.User userDto);

        /// <summary>
        /// Asynchronous remove user.
        /// </summary>
        /// <param name="id">User's id.</param>
        /// <returns>Task.</returns>
        Task Remove(uint id);
    }
}