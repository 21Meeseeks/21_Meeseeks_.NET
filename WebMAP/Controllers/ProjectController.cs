using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Domain.Entity;
using Service;
using WebMAP.Models;
using Rotativa;
using System.Globalization;

//using WebMAP.
//using WebMAP.Rotativa;

namespace WebMAP.Controllers
{
    public class ProjectController : Controller
    {
        // GET: Project
        public ActionResult Index()
        {
            HttpClient HTTPC = new HttpClient();
            HTTPC.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/rest/");
            HTTPC.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage HTTPRM = HTTPC.GetAsync("organigram").Result;
            if (HTTPRM.IsSuccessStatusCode)
            {
                IEnumerable<organigram> plist= HTTPRM.Content.ReadAsAsync<IEnumerable<organigram>>().Result;
                List<organigram> l1 = new List<organigram>();
                List<organigram> l2 = new List<organigram>();
               int s = plist.Count();
                int m = s / 2;
                foreach (var item in plist)
                {
                    System.Diagnostics.Debug.WriteLine(item.programName);

                }
                for (int i = 0; i < m; i++)
                {

                    l1.Add(plist.ElementAt<organigram>(i));
                }
                for (int i = m; i < s; i++)
                {

                    l2.Add(plist.ElementAt<organigram>(i));
                }
                ViewBag.projects1 = plist;
                ViewBag.projects2 = l2;

            }
            else
            {
                ViewBag.result = "Error !";
            }
          
            
            return View();
        }

        // GET: Project/Details/5
        public ActionResult Details(int id)
        {
            HttpClient HTTPC = new HttpClient();
            HTTPC.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/rest/");
            HTTPC.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage HTTPRM = HTTPC.GetAsync("organigram").Result;
            if (HTTPRM.IsSuccessStatusCode)
            {
                IEnumerable<organigram> plist = HTTPRM.Content.ReadAsAsync<IEnumerable<organigram>>().Result;
                organigram or =plist.First(c => c.idOrganigram == id);
                ViewBag.result = or;
                return View();
            }

            return View();
        }

        // GET: Project/pdf/5
        public ActionResult GeneratePDF(int id)
        {
            return new Rotativa.ActionAsPdf("Details", new { id = id });
        }

        /*     public ActionResult GeneratePDF(int id)
             {
            //     return new Rotativa.ActionAsPdf("IdCardPreview", new { empid = empid });
             }
             */


        // GET: Project/Create
        public ActionResult Create()
        {//localhost:18080/21meeseeks-web/rest/project/competence
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("rest/client").Result;
            //localhost:18080/21meeseeks-web/rest/client
            ViewBag.clients = response.Content.ReadAsAsync<IEnumerable<Client>>().Result;


            //localhost:18080/21meeseeks-web/rest/project/competence
                HttpClient client2 = new HttpClient();
                client2.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/");
                client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response2 = client2.GetAsync("rest/competence").Result;
                //localhost:18080/21meeseeks-web/rest/client
               IEnumerable<Competence> c = response2.Content.ReadAsAsync<IEnumerable<Competence>>().Result;



      
            ViewBag.competences = c;
            ViewBag.competencesnumber = c.Count();
            return View();
        }

        // POST: Project/Create
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Create(FormCollection collection)
        {

            var clientID = collection["client"];
            int id_client = int.Parse(clientID);
            project p = new project();
            p.client_idUser = int.Parse(clientID);
            p.dateStart = DateTime.ParseExact(collection["startdate"]
                  ,
                  "MM/dd/yyyy",
                  System.Globalization.CultureInfo.InvariantCulture);
            p.dateEnd = DateTime.ParseExact(collection["enddate"]
                  ,
                  "MM/dd/yyyy",
                  System.Globalization.CultureInfo.InvariantCulture);
            p.description = collection["description"];
            p.name = collection["projectname"];

            Project pro = new Project();
            pro.client = new Client()
            {
                idUser = id_client

            };
            //pro.dateStart = p.dateStart;
            //pro.dateEnd = p.dateEnd;
            pro.description = p.description;
            pro.name = p.name;
            pro.dateEnd = DateTime.ParseExact(p.dateEnd.Value.ToString("MM/dd/yyyy"), "yyyy-MM-dd",
                                   CultureInfo.InvariantCulture); 
            pro.dateStart = DateTime.ParseExact(p.dateStart.Value.ToString("MM/dd/yyyy"), "yyyy-MM-dd",
                                   CultureInfo.InvariantCulture);

            string cp = collection["skills"];


            HttpClient HTTPC = new HttpClient();



            HTTPC.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/rest/");
            HTTPC.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var R = await HTTPC.PostAsJsonAsync<Project>("project", pro);
            project projet = R.Content.ReadAsAsync<project>().Result;
            System.Diagnostics.Debug.WriteLine(projet.idProject);


            /*    List<competence> LC = new List<competence>();
                if (cp != null)
                {
                    string[] cps = cp.Split(',');
                    foreach (var c in cps)
                    {


                    using (CompetenceService cs = new CompetenceService())
                    {
                        int id = int.Parse(c);

                        competence c2 = cs.Get(a => a.idCompetence == id);
                        LC.Add(c2);
                        //your code
                    }
                  
                    }
                }
                p.competences = LC;

            ProjectService ps = new ProjectService();
            
                ps.Add(p);
                ps.Commit();
              */
            ProjectService ps = new ProjectService();

            // Project proj = ps.GetMany();
            /*new Project()
        {
            idProject = pro.idProject
        };*/

            Organigram o = new Organigram()
            {
                programName = collection["programname"],
                financialManager = collection["financialmanager"],
                projectManagerName = collection["projectmanager"],
                assignmentManager = collection["assignmentmanager"],
                project =new Project() { idProject = projet.idProject }

            };

            HttpClient client2 = new HttpClient();

            //   cc.clientType = "NEW_CLIENT";
            client2.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/rest/");

            await client2.PostAsJsonAsync<Organigram>("organigram", o).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());


            /*
              
            organigram o = new organigram()
            {   
                client_idUser = int.Parse(collection["client"]),
                projectName = collection["projectname"],
                programName = collection["programname"],
                financialManager=collection["financialmanager"],
                projectManagerName=collection["projectmanager"],
                assignmentManager = collection["assignmentmanager"]

            };
            // TODO: Add insert logic here
            using (OrganigramService os = new OrganigramService())
            {
                os.Add(o);
                os.Commit();
            }*/
            return RedirectToAction("Index");

        }


        public static bool CommonString(string left, string right)
        {
            List<string> result = new List<string>();
            string[] rightArray = right.Split(' ');
            string[] leftArray = left.Split(' ');

            result.AddRange(rightArray.Where(r => leftArray.Any(l => l.StartsWith(r))));

            // must check other way in case left array contains smaller words than right array
           result.AddRange(leftArray.Where(l => rightArray.Any(r => r.StartsWith(l))));

            return (result.Distinct().ToArray().Count() != 0) ;
        }


        // GET: Project/competence/suggest
        public JsonResult SearchCompetence(string query)
        {
            //localhost:18080/21meeseeks-web/rest/project/competence
            HttpClient client2 = new HttpClient();
            client2.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/");
            client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response2 = client2.GetAsync("rest/competence").Result;
            //localhost:18080/21meeseeks-web/rest/client
            IEnumerable<Competence> c = response2.Content.ReadAsAsync<IEnumerable<Competence>>().Result;

            IEnumerable<competence> StuList;
            CompetenceService cs = new CompetenceService();
            //a => CommonString(query,a.description)
            StuList = cs.GetMany()    ;
            var suggestions = c.Select(d =>CommonString(d.description,query));

    return Json(suggestions, JsonRequestBehavior.AllowGet);
         
     
        }





        // GET: Project/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Project/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Project/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Project/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
