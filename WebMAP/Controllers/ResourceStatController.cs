using Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebMAP.Models;
using static WebMAP.Controllers.ProjectStatsController;

namespace WebMAP.Controllers
{
    public class ResourceStatController : Controller
    {
        ISkillsStatService ISS;
        IClientStatService ICS;
        // GET: Resource
        public async System.Threading.Tasks.Task<ActionResult> Index()
        {
            /*
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await Client.GetAsync("rest/project");
            string data = await response.Content.ReadAsStringAsync();
            JavaScriptSerializer JSserializer = new JavaScriptSerializer();
            ViewBag.result = JSserializer.Deserialize<IEnumerable<Project>>(data);
            //response = Client.GetAsync("rest/")*/

            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await Client.GetAsync("rest/resources/resource/employee");
            string data = await response.Content.ReadAsStringAsync();
            ViewBag.countemployee = data;

            IDictionary<string, int> x = new Dictionary<string, int>();
            //x.Add();
            response = await Client.GetAsync("rest/resources/resource/freelancer");
            data = await response.Content.ReadAsStringAsync();
            
            ViewBag.countfreelancer = data;
            
            return View();
        }
        public ActionResult Map()
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            ICS = new ClientStatService();
            var count = 0;
            Dictionary<dynamic, int> adressResource = new Dictionary<dynamic, int>();
            foreach (var x in ICS.getListOfAdress())
            {
                HttpResponseMessage response = Client.GetAsync("rest/resources/resource/adress/" + x).Result;
                string data = response.Content.ReadAsStringAsync().Result;
                var serializer = new JavaScriptSerializer();
                var deserailizedResult = serializer.Deserialize<List<object>>(data);
                var y = ProjectStatsController.GeoCodeAPI(x);
                dynamic f = ReverseGeoCodeAPI(y);
                if (deserailizedResult.Count()>0)
                    //count += deserailizedResult.Count();new { code = f.address.country_code, adress = f.address.country }
                    adressResource.Add(f, deserailizedResult.Count());
            }
            ViewBag.Resources = adressResource;
            return PartialView();
        }
        public ActionResult DynamicRation()
        {

           
            ISS = new SkillsStatService();
            List<Skill> listSkill = new List<Skill>();
            foreach (var x in ISS.GetMany())
            {
                listSkill.Add(new Skill { idCompetence = x.idCompetence, Label = x.Label });
            }
            List<Seniority> listSeniority = new List<Seniority>();
            foreach (var x in ISS.getSeniority())
            {
                listSeniority.Add(new Seniority { idSeniority = x.idSeniority, Label = x.name });
            }

            ViewBag.ListSkills = listSkill;
            ViewBag.ListSen = listSeniority;
            
            return PartialView(listSkill);
        }
        [HttpPost]
        public ActionResult getRatio(string competence ,  string seniority , bool available , int level , bool dayoff)
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string uri = "rest/resources?";
            if (competence != "")
                uri += "competence="+ competence + "&";
            if (available)
                uri += "availability=available" + "&";
            if (dayoff)
                uri += "dayoff=true" + "&";
            if (seniority != "")
                uri += "seniority=" + seniority + "&";
            if (level > 0 )
                uri += "level=" + level + "&";
            if (uri.EndsWith("&") || uri.EndsWith("?"))
                uri = uri.Remove(uri.Length - 1);
            

            HttpResponseMessage response = Client.GetAsync(uri).Result;
            string data = response.Content.ReadAsStringAsync().Result;

            
            //The Month Selected should change
            data = data.Substring(0, data.Length-1) ;
            try{
                data = data.Substring(0, 4) + "%";
            }catch (Exception e)
            {
                data += "%";
            }
            return Json(new JsonResult()
            {
                Data = new { Result = data }
            }, JsonRequestBehavior.AllowGet);
        }
        [NonAction]
        public dynamic ReverseGeoCodeAPI(GeoCode geo)
        {

            HttpClient Client2 = new HttpClient();

            Client2.BaseAddress = new Uri("https://eu1.locationiq.com/v1/");
            var token = "04e33807847d74";
            //Will Change to be , Client Adress
            var adress = "Kabaria";
            System.Web.HttpUtility.UrlEncode(adress);
            var resUri = "reverse.php?key=" + token + "&lat="+geo.lat+"&lon="+geo.lon+"&format=json";
            HttpResponseMessage response = Client2.GetAsync(resUri).Result;
            string data = response.Content.ReadAsStringAsync().Result;
            var serializer = new JavaScriptSerializer();
            var deserailizedResult = serializer.Deserialize<List<GeoCode>>(data);
            dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject(data);
            
            return result;

        }
    }
}