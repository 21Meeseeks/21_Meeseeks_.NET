using Data.Infrastructures;
using Domain.Entity;
using Service.Pattern;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class TestService : Service<resource>, ITestService 
    {
        static IDataBaseFactory Factory = new DataBaseFactory();

        static IUnitOfWork utk = new UnitOfWork(Factory);
        public TestService() : base(utk)
        {
        }

        public int countResource()
        {
            var x = "HELLO";
            Console.WriteLine(x);
            return GetMany().Count();
        }


        public bool isChanged(ref IEnumerable r) {
            return GetMany().Except((IEnumerable<resource>)r).ToList().Count > 0;
        }
    }
}
