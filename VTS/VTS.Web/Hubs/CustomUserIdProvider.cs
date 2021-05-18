using Microsoft.AspNetCore.SignalR;
using VTS.Core.Constants;

namespace VTS.Web.Hubs
{
    /// <summary>
    /// Class for configuring UserId for a connection to a SignalRHub.
    /// </summary>
    public class CustomUserIdProvider : IUserIdProvider
    {
        /// <summary>
        /// Method for configuring UserId for a connection to a SignalRHub.
        /// </summary>
        /// <param name="connection">connection to a SignalRHub.</param>
        /// <returns>string UserId.</returns>
        public string GetUserId(HubConnectionContext connection)
        {
            if (connection.User.Identity.IsAuthenticated)
            {
                return connection.User.FindFirst(ClaimKeys.Id).Value;
            }

            return "";
        }
    }
}
