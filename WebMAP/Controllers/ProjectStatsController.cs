using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebMAP.Models;

namespace WebMAP.Controllers
{
    public class ProjectStatsController : Controller
    {
        // GET: ProjectStats
        public ActionResult Index()
        {

            IProjectStatService PSS = new ProjectStatService();
            List<GeoCode> list = new List<GeoCode>();
            //var resultTable = PSS.ProjectsWithAdress();
            foreach (var row in PSS.ProjectsWithAdress())
            {
                var code = GeoCodeAPI(row.Key);
                var lat = code.lat;
                foreach (var project in row.Value)
                {
                    GeoCode geo = new GeoCode { lat = code.lat , lon = code.lon , project = new Project2
                    {
                        name = project.name,
                        description = project.description,
                        idProject = project.idProject,
                        dateEnd = project.dateEnd,
                        client = project.client.clientName
                    }
                };  
                    code.project = new Project2
                    {
                        name = project.name,
                        description = project.description,
                        idProject = project.idProject,
                        dateEnd = project.dateEnd,
                        client = project.client.clientName
                    };
                    list.Add(geo);
                }
            }
            ViewBag.ListProject = list;
            return View();
        }

        [NonAction]
        public static GeoCode GeoCodeAPI(string adressProject)
        {
            HttpClient Client2 = new HttpClient();

            Client2.BaseAddress = new Uri("https://eu1.locationiq.com/v1/");
            var token = "04e33807847d74";
            //Will Change to be , Client Adress
            var adress = "Kabaria";
            System.Web.HttpUtility.UrlEncode(adress);
            var resUri = "search.php?key=" + token + "&q=" + System.Web.HttpUtility.UrlEncode(adressProject) + "&format=json";
            HttpResponseMessage response = Client2.GetAsync(resUri).Result;
            string data = response.Content.ReadAsStringAsync().Result;
            var serializer = new JavaScriptSerializer();
            var deserailizedResult = serializer.Deserialize<List<GeoCode>>(data);
            dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject(data);
            var lat = result[0].lat.ToString();
            return new GeoCode
            {
                lat = double.Parse(result[0].lat.ToString(), System.Globalization.CultureInfo.InvariantCulture),
                lon = double.Parse(result[0].lon.ToString(), System.Globalization.CultureInfo.InvariantCulture)

            };

        }
        public class GeoCode
        {
            // To Deserialize The JSON
            string place_id { get; set; }
            string licence { get; set; }
            List<string> boundingbox { get; set; }
            public double lat { get; set; }
            public double lon { get; set; }
            string display_name { get; set; }
            public Project2 project { get; set; }
        }
    }
}