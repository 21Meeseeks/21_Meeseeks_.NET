using Domain.Entity;
using Microsoft.AspNet.SignalR;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebMAP.Models;

namespace WebMAP
{
    public class ChatHub : Hub
    {
        private readonly static ConnectionMapping<string> _connections = new ConnectionMapping<string>();

        public void SendChatMessage(string sender, string email, string message, string clientName)
        {
            foreach (var connectionId in _connections.GetConnections(email))
            {
                Clients.Client(connectionId).addChatMessage(sender, email, message, clientName);
            }
            user us = new user();
            user ur = new user();
            clientservice CS = new clientservice();
            ResourceService RS = new ResourceService();
            client cls = CS.Get(x => x.email.Equals(sender));
            if (cls == null)
            {
                resource rss = RS.Get(x => x.email.Equals(sender));
                us.email = rss.email;
                us.idUser = rss.idUser;
            }
            else
            {
                us.email = cls.email;
                us.idUser = cls.idUser;
            }
            client clr = CS.Get(x => x.email.Equals(email));
            if (clr == null)
            {
                resource rsr = RS.Get(x => x.email.Equals(email));
                ur.email = rsr.email;
                ur.idUser = rsr.idUser;
            }
            else
            {
                ur.email = clr.email;
                ur.idUser = clr.idUser;
            }
            ConversationUsersService CUS = new ConversationUsersService();
            IEnumerable<conversation_user> conversationsuserslist = CUS.GetMany();
            ConversationService COS = new ConversationService();
            IEnumerable<conversation> conversationslist = COS.GetMany();
            conversation c0 = null;
            int s = 0;
            foreach (conversation c in conversationslist)
            {
                s = 0;
                foreach (conversation_user cu in conversationsuserslist)
                {
                    if (cu.Conversation_idConversation == c.idConversation && (cu.participants_idUser == us.idUser || cu.participants_idUser == ur.idUser))
                    {
                        s++;
                    }
                }
                if (s > 1)
                {
                    c0 = c;
                    break;
                }
            }
            if (c0 == null)
            {
                c0 = new conversation
                {
                    startDate = DateTime.Now
                };
                conversation_user cu1 = new conversation_user
                {
                    conversation = c0,
                    Conversation_idConversation = c0.idConversation,
                    participants_idUser = us.idUser
                };
                conversation_user cu2 = new conversation_user
                {
                    conversation = c0,
                    Conversation_idConversation = c0.idConversation,
                    participants_idUser = ur.idUser
                };
                c0.conversation_user.Add(cu1);
                c0.conversation_user.Add(cu2);
                COS.Add(c0);
                COS.Commit();
            }
            MessageService MS = new MessageService();
            message m = new message
            {
                content = message,
                conversation_idConversation = c0.idConversation,
                sendingDate = DateTime.Now,
                sender_idUser = us.idUser
            };
            message_user mu = new message_user
            {
                Message_idMessage = m.idMessage,
                receivers_idUser = ur.idUser
            };
            m.message_user.Add(mu);
            MS.Add(m);
            MS.Commit();
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

        public List<message> GetConversation(string current, string receiver)
        {
            List<message> LS = new List<message>();
            MessageService MS = new MessageService();
            List<message> LM = MS.GetMany().ToList();
            foreach (message M in LM)
            {
                message m = new message
                {
                    content = M.content,
                    sendingDate = M.sendingDate,
                    sender_idUser = M.sender_idUser
                };
                LS.Add(m);
            }
            return LS;
        }
    }
}