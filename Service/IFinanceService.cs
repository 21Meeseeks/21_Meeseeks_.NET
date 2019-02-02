using Domain.Entity;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IFinanceService : IService<term>
    {
        FinanceMonthlyClient getClientOfTheMonth();
        FinanceMonthlyClient getClientOfTheMonth(string month);
        float getTotalProfit();
        float getProfitByYear(int year);
        float getProfitByMonth(int month);
        double getProfitGrowth();
        float getTotalProfitByResource();
        DateTime getFirstMandateDate();
        Dictionary<competence,double> getProfitByCompetence();
        Dictionary<resource, ProjectProgress> getProgressByResource();

    }
}
