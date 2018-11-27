using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //var result = Service.Class1.test();
            TermService ts = new TermService();
            var result = ts.GetMany().ToList();
            Console.ReadKey();
        }
    }
}
