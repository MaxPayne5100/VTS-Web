using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace VTS.Web.Hubs
{
    public class BookingHub : Hub
    {
        public async Task SendMessage(string userId, string message)
        {
            await Clients.User(userId).SendAsync("ReceiveMessage", message);
        }
    }
}
