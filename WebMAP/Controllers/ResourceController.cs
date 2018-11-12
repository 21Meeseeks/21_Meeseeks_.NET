using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebMAP.Models;

namespace WebMAP.Controllers
{
    public class ResourceController : Controller
    {
        // GET: Resource
        public async System.Threading.Tasks.Task<ActionResult> Index()
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await Client.GetAsync("rest/project");
            string data = await response.Content.ReadAsStringAsync();
            JavaScriptSerializer JSserializer = new JavaScriptSerializer();
            ViewBag.result = JSserializer.Deserialize<IEnumerable<Project>>(data);
            response = Client.GetAsync("rest/")
            return View();
        }
    }
}