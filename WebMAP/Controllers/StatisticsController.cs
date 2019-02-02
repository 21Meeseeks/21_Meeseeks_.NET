using Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace WebMAP.Controllers
{
    public class StatisticsController : Controller
    {
        // GET: Statistics
        public ActionResult Index()
        {
            return View();
        }
        public async System.Threading.Tasks.Task<ActionResult> Statistics()
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
           // HttpResponseMessage response = await Client.GetAsync("rest/resources/resource/employee");
           // string data = await response.Content.ReadAsStringAsync();
            ViewBag.countemployee = 30;
            IProjectStatService y = new ProjectStatService();
            ViewBag.year = y.ProjectsNumberBySectorGroupByYear();
            // response = await Client.GetAsync("rest/resources/resource/freelancer");
            //  data = await response.Content.ReadAsStringAsync();
            ViewBag.countfreelancer = 10;
            return View();
        }
        [NonAction]
        public async System.Threading.Tasks.Task<string> consumption()
        {

            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await Client.GetAsync("rest/resources/resource/employee");
            return await response.Content.ReadAsStringAsync();
        }

        [ChildActionOnly]
        public ActionResult AddressTypePartialx()
        {
           return PartialView("Partial");
        }
        [HttpGet]
        public ActionResult AddressTypePartial()
        {

            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
           // HttpResponseMessage response = await Client.GetAsync("rest/resources/resource/employee");
          //  string data = await response.Content.ReadAsStringAsync();
            ViewBag.countemployee = 20;
            
            IDictionary<string, int> x = new Dictionary<string, int>();
            //x.Add();
            //response = await Client.GetAsync("rest/resources/resource/freelancer");
            // data = await response.Content.ReadAsStringAsync();

            IProjectStatService y = new ProjectStatService();
            ViewBag.year = y.ProjectsNumberBySectorGroupByYear();
            ViewBag.countfreelancer = 50;
            return PartialView("DonutChartPartial");

        }
    }
}