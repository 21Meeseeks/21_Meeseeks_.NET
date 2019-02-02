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
    public class ConversationService : Service<conversation>, IConversationService
    {
        static IDataBaseFactory Factory = new DataBaseFactory();
        static IUnitOfWork UTK = new UnitOfWork(Factory);
        public ConversationService() : base(UTK)
        {

        }
    }
}
