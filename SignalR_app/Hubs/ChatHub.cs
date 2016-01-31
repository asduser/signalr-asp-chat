using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using SignalR_app.Models;
using SignalR_app.Enums;

namespace SignalR_app.Hubs
{
    public class ChatHub : Hub
    {
        private ApplicationContext _db = new ApplicationContext();

        static List<User> Users = new List<User>();

        // Send message
        public void Send(string name, string message)
        {
            //var _name = Context.User.Identity.Name != "" ? Context.User.Identity.Name : name;
            var _name = Context.User.Identity.Name != "" ? _db.Users.Where(x => x.Email == Context.User.Identity.Name).ToList()[0].DisplayName : name;

            Clients.All.addMessage(_name, message);
        }

        // Connect user
        public void Connect(string userName)
        {
            var id = Context.ConnectionId;

            //var name = Context.User.Identity.Name != "" ? Context.User.Identity.Name : userName;
            var name = Context.User.Identity.Name != "" ? _db.Users.Where(x => x.Email == Context.User.Identity.Name).ToList()[0].DisplayName : userName;

            if (Users.Any(x => x.ConnectionId == id)) return;
            Users.Add(new User { ConnectionId = id, Name = name });

            // Send message to user
            Clients.Caller.onConnected(id, name, Users, HttpContext.Current.Request.IsAuthenticated);

            // Send to all except specified user
            Clients.AllExcept(id).onNewUserConnected(id, name);
            Clients.Caller.addMessage("Server status:", " Welcome, " + name + "!", ColorConstant.Green.Value);
            Clients.Others.addMessage("Server status:", userName + " is connected.", ColorConstant.Green.Value);
        }

        // Disconnect user
        public override Task OnDisconnected(bool stopCalled)
        {
            var item = Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                Clients.Others.addMessage("Server status:", item.Name + " was disconnected.", ColorConstant.Green.Value);
                Users.Remove(item);
                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.Name, Users);
            }

            return base.OnDisconnected(stopCalled);
        }

        /*public override Task OnConnected()
        {
            return Clients.Caller.addMessage("Welcome " + Context.User.Identity.Name + "!");
        }*/

    }

    public class AuthorizeEchoConnection : PersistentConnection
    {
        protected override bool AuthorizeRequest(IRequest request)
        {
            return request.User != null && request.User.Identity.IsAuthenticated;
        }
    }

}