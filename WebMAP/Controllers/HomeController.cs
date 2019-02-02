using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using WebMAP.Models;

namespace WebMAP.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        // GET: Settings
        public ActionResult Settings()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/rest/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("client/category").Result;
            ViewBag.result = response.Content.ReadAsAsync<IEnumerable<ClientCategory>>().Result;

            HttpClient HTTPC = new HttpClient();
            HTTPC.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/rest/");
            HTTPC.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage HTTPRM = HTTPC.GetAsync("Leavetype").Result;
            ViewBag.leave = HTTPRM.Content.ReadAsAsync<IEnumerable<LeaveType>>().Result;

            return View();
        }
    }
}