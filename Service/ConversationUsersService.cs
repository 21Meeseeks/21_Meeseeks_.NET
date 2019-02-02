using Data.Infrastructures;
using Domain.Entity;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ConversationUsersService : Service<conversation_user>, IConversationUsersService
    {
        static IDataBaseFactory Factory = new DataBaseFactory();
        static IUnitOfWork UTK = new UnitOfWork(Factory);
        public ConversationUsersService() : base(UTK)
        {

        }


    }
}
