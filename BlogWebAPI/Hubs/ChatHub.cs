using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace BlogWebAPI.Hubs
{
    public class ChatHub : Hub
    {
        public ChatHub()
        {

        }
        public async Task sendnotificationstouser(string title, string message,string type)
        {
            await Clients.All.SendAsync("notifyUser_broadcast", title, message, type);
        }

    }
}
