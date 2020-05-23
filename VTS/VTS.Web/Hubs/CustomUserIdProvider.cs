using Microsoft.AspNetCore.SignalR;
using VTS.Core.Constants;

namespace VTS.Web.Hubs
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User.FindFirst(ClaimKeys.Id).Value;
        }
    }
}
