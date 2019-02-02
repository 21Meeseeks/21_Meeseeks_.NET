using Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace WebMAP.Controllers
{
    public class FinanceController : Controller
    {
        IFinanceService IFS = new FinanceService();
        IProjectStatService IPS = new ProjectStatService();
        IClientStatService ICS = new ClientStatService();
        ITestService ITS = new TestService();
        ISkillsStatService ISS = new SkillsStatService();
        // GET: Finance
        public ActionResult Index()
        {
            ViewBag.ThisMonth = DateTimeFormatInfo.CurrentInfo.GetMonthName(DateTime.Now.Month);
            ViewBag.Months = DateTimeFormatInfo.CurrentInfo.MonthNames;
            IFS = new FinanceService();
            ViewBag.Client = IFS.getClientOfTheMonth();

            return View();
        }
        public ActionResult ClientOfTheMonth()
        {
            ViewBag.ThisMonth = DateTimeFormatInfo.CurrentInfo.GetMonthName(DateTime.Now.Month);
            ViewBag.Months = DateTimeFormatInfo.CurrentInfo.MonthNames;
            IFS = new FinanceService();
            ViewBag.Client = IFS.getClientOfTheMonth();

            return PartialView();
        }

        [HttpPost]
        public ActionResult OnGetFilter(string month)
        {
            IFS = new FinanceService();
            //The Month Selected should change

            var x = IFS.getClientOfTheMonth(month);
            var name = "";
            double sum = 0;
            var desc= "None";
            var email = "None";
            string phone = "000000000";
            int nbProjects = 0;
            try
            {
                name = x.Client.clientName;
                sum = (double)x.Sum;
                phone = x.Client.phoneNumber;
                email = x.Client.email;
                desc = x.Client.clientcategory.description;
                nbProjects = x.Client.projects.Count();
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { ErrorMessage = "Could not save because XYZ" });
            }
            return Json(new JsonResult()
            {
                Data = new { name = name, sum = sum , dec = desc , email = email , nbProjects = nbProjects , phone= phone }
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SimpleWidget()
        {
            ViewBag.MemberProfit = IFS.getTotalProfit() / ICS.getNumberClients();
            ViewBag.MonthlyTerm = System.Math.Round((double)IFS.GetMany().Count() / ((DateTime.Now - IFS.getFirstMandateDate()).Days / 7), 4);
            var x = IPS.CountProjectsDone();
            var diff = IPS.CountProjectsDone(DateTime.Now.Year.ToString()) - IPS.CountProjectsDone(DateTime.Now.AddYears(-1).Year.ToString());
            double ratio = System.Math.Round((double)diff / x, 4);
            ViewBag.ProjectIncrease = ratio * 100;
            ViewBag.TotalSkills = ISS.CountSkills();
            ViewBag.ProfitGrowth = IFS.getProfitGrowth();
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //x.Add();
            HttpResponseMessage response = Client.GetAsync("rest/resources/resource/freelancer").Result;
            string data = response.Content.ReadAsStringAsync().Result;

            ViewBag.TotalResources = IPS.countResource();

            ViewBag.countfreelancer = System.Math.Round((double)Convert.ToDouble(data) / IPS.countResource(),4)*100;

            return PartialView();
        }
        public ActionResult SkillsProfit()
        {
            
            ViewBag.Widgets = IFS.getProfitByCompetence();
            return PartialView();
        }
        public ActionResult UserProgress()
        {
            ViewBag.UserProgress = IFS.getProgressByResource();
            
            return PartialView();
        }

    }
}