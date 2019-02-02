using Domain.Entity;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Infrastructures;
using System.Collections;

namespace Service
{
    public class ProjectStatService : Service<project>, IProjectStatService
    {
        static IDataBaseFactory Factory = new DataBaseFactory();

        static IUnitOfWork utk = new UnitOfWork(Factory);
        public ProjectStatService() : base(utk)
        {
        }

        public IDictionary<int, List<Sector>> ProjectsNumberBySectorGroupByYear()
        {
            IDictionary<int, List<Sector>> myDict = new Dictionary<int, List<Sector>>();
            IEnumerable<project> projects= GetMany();
            foreach (var item in projects)
            {
                if (myDict.Keys.Contains(item.dateStart.Value.Year))
                {
                    if (myDict[item.dateStart.Value.Year].Exists(e=>e.sector.Equals(item.projectType)))
                        myDict[item.dateStart.Value.Year].Find(e => e.sector.Equals(item.projectType)).size++;
                    
                    else
                        myDict[item.dateStart.Value.Year].Add(new Sector(item.projectType));
                }else
                    myDict.Add(item.dateStart.Value.Year,new List<Sector>() { new Sector(item.projectType) });
            }
        
            return myDict;
        }

        public IDictionary<string, List<project>> ProjectsWithAdress()
        {
            
            return Factory.DataContext.projects.ToList().Where(e=>e.client!=null).GroupBy(e => e.client.address, e => e, (adress, projects) => new
            {
                Key = adress,
                project = projects.ToList()
            }).ToDictionary(e=>e.Key,e=>e.project);
        }

        public int GetNewProjects()
        {
            return GetMany().Where(e => e.projectType.ToUpper().Equals("NEW")).Count();
        }

        public int CountProjectsDone()
        {
            var x = GetMany().Where(e => e.projectType.ToUpper().Equals("DONE")).Count();
            return GetMany().Where(e => e.projectType.ToUpper().Equals("DONE")).Count();
        }

        public int CountProjectsDone(string year)
        {
            //new DateTime(DateTime.Parse("1." + "1 " + year).Year, DateTime.Now.Month , DateTime.Now.Day);
            return GetMany().Where(e => e.projectType.ToUpper().Equals("DONE") && e.dateEnd <= DateTime.Parse("12/" + "31/" + year)
                                        && DateTime.Parse("12/" + "31/" + year).AddYears(-1).AddDays(1)<= e.dateEnd ).Count();
        }

        public int countResource()
        {
            return Factory.DataContext.resources.Count();
            throw new NotImplementedException();
        }

        public class Sector
        {
            public string sector { get; set; }
            public int size { get; set; }

            public Sector(string sector , int size=1)
            {
                this.sector = sector;
                this.size = size;
            }

            public Sector()
            {
            }
        }
        public class AdressProject{
            public string adress { get; set; }
        }
    }
}
