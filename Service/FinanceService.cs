using Data.Infrastructures;
using Domain.Entity;
using MySql.Data.MySqlClient;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class FinanceService : Service<term>, IFinanceService
    {
        static IDataBaseFactory Factory = new DataBaseFactory();

        static IUnitOfWork utk = new UnitOfWork(Factory);
        public FinanceService() : base(utk)
        {
        }

        public FinanceMonthlyClient getClientOfTheMonth()
        {
            var firstDayOfThisMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var lastDayOfThisMonth = firstDayOfThisMonth.AddMonths(1).AddDays(-1);
            
                var x = GetMany(e => DateTime.Compare((DateTime)e.dateStart,firstDayOfThisMonth)>0 && DateTime.Compare((DateTime)e.dateStart, lastDayOfThisMonth) < 0
                                && DateTime.Compare((DateTime)e.dateEnd, firstDayOfThisMonth) > 0 && DateTime.Compare((DateTime)e.dateEnd, lastDayOfThisMonth) < 0);

            Commit();
            /*var y = x.GroupBy(e => e.project.client, e => e.dailyFee, (client, fees) => new
                {
                    Sum = fees.Sum()
                });
           */
           //Check if The Use of Factory.DataContext is allowed or not
            var y = Factory.DataContext.terms.GroupBy(e => e.project.client, e => e.dailyFee, (client, fees) => new
            {
                Sum = fees.Sum()
            }).OrderByDescending(e=>e.Sum).First();
            return Factory.DataContext.terms.GroupBy(e => e.project.client, e => e, (client, terms) => new FinanceMonthlyClient
            {
                Sum = terms.Sum(e=>e.dailyFee*e.numberofDaysTerm),
                Client = client
            }).OrderByDescending(e => e.Sum).First(); 
        }

        public FinanceMonthlyClient getClientOfTheMonth(string monthName)
        {
            var firstDayOfThisMonth = new DateTime(DateTime.Today.Year, DateTime.Parse("1." + monthName + " 2008").Month , 1);
            var lastDayOfThisMonth = firstDayOfThisMonth.AddMonths(1).AddDays(-1);
            var x = Factory.DataContext.terms.Where(e => (DateTime.Compare((DateTime)e.dateStart, firstDayOfThisMonth) > 0 && DateTime.Compare((DateTime)e.dateStart, lastDayOfThisMonth) < 0)
                                   || (DateTime.Compare((DateTime)e.dateEnd, firstDayOfThisMonth) > 0 && DateTime.Compare((DateTime)e.dateEnd, lastDayOfThisMonth) < 0)
                                   || (DateTime.Compare((DateTime)e.dateStart, firstDayOfThisMonth) < 0 && DateTime.Compare((DateTime)e.dateEnd, lastDayOfThisMonth) > 0)).
                   GroupBy(e => e.project.client, e => e, (client, terms) => new FinanceMonthlyClient
                   {
                       Sum = Math.Round((double)terms.Sum(e => e.dailyFee * e.numberofDaysTerm)) ,
                       Client = client
                   }).OrderByDescending(e => e.Sum).First();

            try
            { 
                return Factory.DataContext.terms.Where(e => (DateTime.Compare((DateTime)e.dateStart, firstDayOfThisMonth) > 0 && DateTime.Compare((DateTime)e.dateStart, lastDayOfThisMonth) < 0)
                                   || (DateTime.Compare((DateTime)e.dateEnd, firstDayOfThisMonth) > 0 && DateTime.Compare((DateTime)e.dateEnd, lastDayOfThisMonth) < 0)
                                   || (DateTime.Compare((DateTime)e.dateStart, firstDayOfThisMonth) < 0 && DateTime.Compare((DateTime)e.dateEnd, lastDayOfThisMonth) > 0)).
                   GroupBy(e => e.project.client, e => e, (client, terms) => new FinanceMonthlyClient
                   {
                       Sum = Math.Round((double)terms.Sum(e => e.dailyFee * e.numberofDaysTerm)),
                       Client = client
                   }).OrderByDescending(e => e.Sum).First();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public DateTime getFirstMandateDate()
        {
            return (DateTime)GetMany().Where(e=>e.dateStart!=null).OrderBy(e => e.dateStart).First().dateStart;
        }

        public Dictionary<competence, double> getProfitByCompetence()
        {
            Dictionary<competence, double> dict = new Dictionary<competence, double>();
            foreach (var y in Factory.DataContext.competences.ToList())
                dict.Add(y,y.projects.Sum(e=>e.terms.Sum(t=>t.dailyFee*t.numberofDaysTerm)));
            return dict;
            throw new NotImplementedException();
        }

        public float getProfitByMonth(int month)
        {
            throw new NotImplementedException();
        }

        public float getProfitByYear(int year)
        {
            return GetMany().Where(e => e.dateStart >= DateTime.Parse("12/" + "31/" + year).AddYears(-1).AddDays(1) && e.dateStart <= DateTime.Parse("12/" + "31/" + year)).Sum(e => e.dailyFee * e.numberofDaysTerm);
        }

        public double getProfitGrowth()
        {
            return System.Math.Round(((double)(getProfitByYear(DateTime.Now.Year) - getProfitByYear(DateTime.Now.Year - 1)) / getTotalProfit()),4) * 100;
        }

        public Dictionary<resource, ProjectProgress> getProgressByResource()
        {
            
            return GetMany().ToList().Where(e => e.dateEnd > DateTime.Now && e.dateStart < DateTime.Now).
                GroupBy(e => e.resource, e => e, (resource, term) => new
                {
                    Term = term.Where(e => e.dateEnd > DateTime.Now && e.dateStart < DateTime.Now).First(),
                    Resource = resource,
                    progress = System.Math.Round((double)(DateTime.Now - term.Where(e => e.dateEnd > DateTime.Now && e.dateStart < DateTime.Now).First().dateStart).Value.Days / 
                                                (term.Where(e => e.dateEnd > DateTime.Now && e.dateStart < DateTime.Now).First().dateEnd -
                                                term.Where(e => e.dateEnd > DateTime.Now && e.dateStart < DateTime.Now).First().dateStart).Value.Days,4)*100
                }).ToDictionary(e => e.Resource, e => new ProjectProgress { progress = e.progress, Project = e.Term.project });
        }

        public float getTotalProfit()
        {
            return GetMany().Where(e => e.dateStart < DateTime.Now).Sum(e => e.dailyFee * e.numberofDaysTerm);
        }

        public float getTotalProfitByResource()
        {

            throw new NotImplementedException();
        }
    }
    public class FinanceMonthlyClient
    {
        public double Sum { get; set; }
        public client Client { get; set; }
    }
    public class ProjectProgress
    {
        public double progress { get; set; }
        public project Project { get; set; }
    }

}
