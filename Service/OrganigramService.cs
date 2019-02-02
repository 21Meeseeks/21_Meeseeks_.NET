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
   public class OrganigramService:Service<organigram>,IOrganigramService
    {
        static IDataBaseFactory Factory = new DataBaseFactory();
        static IUnitOfWork utk = new UnitOfWork(Factory);
        public OrganigramService() : base(utk)
        {
        }
    }
}
