using System.Security.Claims;
using System.Threading.Tasks;

namespace VTS.Services.AuthenticationService
{
    public interface IAuthenticationService
    {
        Task<ClaimsPrincipal> LogIn(uint id, string email);
    }
}