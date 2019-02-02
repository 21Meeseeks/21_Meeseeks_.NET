using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Infrastructures;
using Domain.Entity;
using Service.Pattern;

namespace Service
{
    public class ClientTypeService : Service<clienttype>, IClientTypeService
    {
        static IDataBaseFactory Factory = new DataBaseFactory();
        static IUnitOfWork utk = new UnitOfWork(Factory);
        public ClientTypeService() : base(utk)
        {
        }
    }
}
