using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using VTS.Core.Constants;
using VTS.Repos.UnitOfWork;

namespace VTS.Services.AuthenticationService
{
    /// <summary>
    /// Service for Authentication.
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationService"/> class.
        /// </summary>
        /// <param name="unitOfWork">Unit of Work.</param>
        public AuthenticationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
        }

        /// <inheritdoc />
        public async Task<ClaimsPrincipal> LogIn(uint id, string email)
        {
            var user = await _unitOfWork.Users.FindByEmail(email);

            if (user != null && user.Id == id)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimKeys.Id, user.Id.ToString()),
                    new Claim(ClaimKeys.FirstName, user.FirstName),
                    new Claim(ClaimKeys.LastName, user.LastName),
                    new Claim(ClaimKeys.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                return new ClaimsPrincipal(claimsIdentity);
            }

            throw new ArgumentException("Wrong id or email");
        }
    }
}