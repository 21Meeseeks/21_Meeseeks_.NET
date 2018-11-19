using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Infrastructures
{
    public interface IDataBaseFactory :IDisposable
    {
       Model1 DataContext { get;  }
    }
}
