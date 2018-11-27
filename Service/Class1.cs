using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class Class1
    {
        public static List<term> test()
        {
            Model1 ctx = new Model1();
            return ctx.terms.ToList();

        }
    }
}
