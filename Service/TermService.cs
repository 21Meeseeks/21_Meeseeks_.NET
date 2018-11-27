using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Service.Pattern;
using Domain.Entity;
using Data.Infrastructures;

namespace Service
{
    public class TermService : Pattern.Service<term>
    {
        public TermService()
            : base(new UnitOfWork(new DataBaseFactory()))
        {

        }
    }
}
