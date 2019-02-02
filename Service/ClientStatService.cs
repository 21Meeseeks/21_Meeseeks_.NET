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
    public class ClientStatService : Service<client>, IClientStatService
    {
        static IDataBaseFactory Factory = new DataBaseFactory();

        static IUnitOfWork utk = new UnitOfWork(Factory);
        public ClientStatService() : base(utk)
        {
        }

        public List<string> getListOfAdress()
        {
            return GetMany().Select(e=>e.address).ToList();
            throw new NotImplementedException();
        }

        public int getNumberClients()
        {
            return GetMany().Count();
        }

        public int getNumberClientsOnGoing()
        {
            throw new NotImplementedException();
        }

        public int getNumberNewClients()
        {
            return GetMany().Where(e=>e.clientType.ToUpper().Equals("NEW_CLIENT")).Count();
        }
    }
}
