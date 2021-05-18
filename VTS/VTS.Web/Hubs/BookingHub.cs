using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace VTS.Web.Hubs
{
    /// <summary>
    /// Class for SignalR implementation.
    /// </summary>
    public class BookingHub : Hub
    {
        /// <summary>
        /// Method for sending message using SignalR.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="message">Message to show.</param>
        /// <returns>Task.</returns>
        public async Task SendMessage(string userId, string message)
        {
            await Clients.User(userId).SendAsync("ReceiveMessage", message);
        }
    }
}
