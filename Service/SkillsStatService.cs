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
    public class SkillsStatService : Service<competence> , ISkillsStatService
    {
        static IDataBaseFactory Factory = new DataBaseFactory();

        static IUnitOfWork utk = new UnitOfWork(Factory);
        public SkillsStatService() : base(utk)
        {
        }

        public int CountSkills()
        {
            return GetMany().Count();
        }

        public List<seniority> getSeniority()
        {
            return Factory.DataContext.seniorities.ToList();
        }
    }
}
