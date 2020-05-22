using System.Security.Claims;
using System.Threading.Tasks;

namespace VTS.Services.AuthenticationService
{
    /// <summary>
    /// Interface for Authentication service.
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Log in user.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <param name="email">Email.</param>
        /// <returns>User claims.</returns>
        Task<ClaimsPrincipal> LogIn(int id, string email);
    }
}