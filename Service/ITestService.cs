using Domain.Entity;
using Service.Pattern;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface ITestService : IService<resource>
    {
        int countResource();
        bool isChanged(ref IEnumerable r);
    }


}
