using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebMAP
{
    public class ChatHub : Hub
    {
        private readonly static ConnectionMapping<string> _connections = new ConnectionMapping<string>();

        public void SendChatMessage(string sender, string email, string message)
        {
            foreach (var connectionId in _connections.GetConnections(email))
            {
                Clients.Client(connectionId).addChatMessage(sender, email, message);
            }
        }

        public string Connect(string Email)
        {
            _connections.Add(Email, Context.ConnectionId);

            int number = _connections.Count;

            return Email;
        }

        public string Contact(string Email)
        {
            _connections.Add(Email, Context.ConnectionId);

            int number = _connections.Count;

            return Email;
        }

        public List<string> ConnectedList()
        {
            return _connections.GetAllConnections();
        }
    }
}