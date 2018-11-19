using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using WebMAP.Models;

namespace WebMAP.Controllers
{
    public class RequestController : Controller
    {
        // GET: Request
        public ActionResult Index()
        {
            HttpClient HTTPC = new HttpClient();
            HTTPC.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/rest/");
            HTTPC.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage HTTPRM = HTTPC.GetAsync("Request").Result;
            if (HTTPRM.IsSuccessStatusCode)
            {
                ViewBag.result = HTTPRM.Content.ReadAsAsync<IEnumerable<ProjectRequest>>().Result;
            }
            else
            {
                ViewBag.result = "Error !";
            }
            return View();
        }

        public ActionResult IndexArchived()
        {
            HttpClient HTTPC = new HttpClient();
            HTTPC.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/rest/");
            HTTPC.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage HTTPRM = HTTPC.GetAsync("Request/archive").Result;
            if (HTTPRM.IsSuccessStatusCode)
            {
                ViewBag.result = HTTPRM.Content.ReadAsAsync<IEnumerable<ProjectRequest>>().Result;
            }
            else
            {
                ViewBag.result = "Error !";
            }
            return View("IndexArchived");
        }

        public async System.Threading.Tasks.Task<ActionResult> Details(int ID)
        {
            ProjectRequest PR = new ProjectRequest
            {
                idRequest = ID
            };
            HttpClient HTTPC = new HttpClient();
            HTTPC.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/rest/");
            HTTPC.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var R = await HTTPC.PostAsJsonAsync<ProjectRequest>("Request/findone", PR);
            ViewBag.result = R.Content.ReadAsAsync<ProjectRequest>().Result;
            return View("Details");
        }

        [HttpGet]
        public ActionResult Create()
        {
            HttpClient HTTPC = new HttpClient();
            HTTPC.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/rest/");
            HTTPC.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage HTTPRM = HTTPC.GetAsync("project/test/competences").Result;
            if (HTTPRM.IsSuccessStatusCode)
            {
                ViewBag.result = HTTPRM.Content.ReadAsAsync<IEnumerable<Competence>>().Result;
            }
            else
            {
                ViewBag.result = "Error !";
            }
            return View("Create");
        }

        // GET: Request/Update
        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> Update(int ID)
        {
            ProjectRequest PR = new ProjectRequest
            {
                idRequest = ID
            };
            HttpClient HTTPC = new HttpClient();
            HTTPC.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/rest/");
            HTTPC.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var R = await HTTPC.PostAsJsonAsync<ProjectRequest>("Request/findone", PR);
            ViewBag.request = R.Content.ReadAsAsync<ProjectRequest>().Result;
            ViewBag.startDate = ConvertDate(ViewBag.request.dateStartProject.ToString());
            ViewBag.endDate = ConvertDate(ViewBag.request.dateEndProject.ToString());
            HttpClient HTTPC0 = new HttpClient();
            HTTPC0.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/rest/");
            HTTPC0.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage HTTPRM = HTTPC0.GetAsync("project/test/competences").Result;
            if (HTTPRM.IsSuccessStatusCode)
            {
                ViewBag.result = HTTPRM.Content.ReadAsAsync<IEnumerable<Competence>>().Result;
                List<SelectListItem> Competences = new List<SelectListItem>();
                foreach (var c in ViewBag.result)
                {
                    SelectListItem SLI = new SelectListItem { Text = c.label, Value = c.idCompetence + "" };
                    foreach (var cm in ViewBag.request.competences)
                    {
                        if (c.idCompetence == cm.idCompetence)
                        {
                            SLI.Selected = true;
                        }
                    }
                    Competences.Add(SLI);
                }
                ViewBag.competences = Competences;
            }
            else
            {
                ViewBag.result = "Error !";
            }
            return View("Update", ViewBag.request);
        }

        [HttpPost]
        public ActionResult Create(ProjectRequest PR)
        {
            var css = new List<Competence>();
            HttpClient HTTPC = new HttpClient();
            HTTPC.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/rest/");
            HTTPC.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage HTTPRM = HTTPC.GetAsync("project/test/competences").Result;
            if (HTTPRM.IsSuccessStatusCode)
            {
                var cs = HTTPRM.Content.ReadAsAsync<IEnumerable<Competence>>().Result;
                foreach (var c in Request.Form["CompetenceList"])
                {
                    foreach (var cmptnc in cs)
                    {
                        if(cmptnc.idCompetence.ToString().Equals(c.ToString()))
                        {
                            css.Add(cmptnc);
                        }
                    }
                }
                PR.competences = css;
            }
            HttpClient HTTPC0 = new HttpClient();
            HTTPC0.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/rest/");
            HTTPC0.PostAsJsonAsync<ProjectRequest>("Request", PR).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
            System.Threading.Thread.Sleep(3000);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Update(ProjectRequest PR)
        {
            string cp = Request.Form["competences"];
            List<Competence> LC = new List<Competence>();
            if (cp != null)
            {
                string[] cps = cp.Split(',');
                foreach (var c in cps)
                {
                    int id = int.Parse(c);
                    Competence Comp = new Competence
                    {
                        idCompetence = id
                    };
                    LC.Add(Comp);
                }
            }
            PR.competences = LC;
            HttpClient HTTPC = new HttpClient();
            HTTPC.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/rest/");
            HTTPC.PutAsJsonAsync<ProjectRequest>("Request", PR).ContinueWith((task) => task.Result.EnsureSuccessStatusCode());
            return RedirectToAction("Index");
        }

        public ActionResult Archive(int ID)
        {
            ProjectRequest PR = new ProjectRequest
            {
                idRequest = ID
            };
            HttpClient HTTPC = new HttpClient();
            HTTPC.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/rest/");
            HTTPC.PostAsJsonAsync<ProjectRequest>("Request/remove", PR).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
            System.Threading.Thread.Sleep(3000);
            return RedirectToAction("Index");
        }

        public ActionResult Unarchive(int ID)
        {
            ProjectRequest PR = new ProjectRequest
            {
                idRequest = ID
            };
            HttpClient HTTPC = new HttpClient();
            HTTPC.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/rest/");
            HTTPC.PostAsJsonAsync<ProjectRequest>("Request/dearchive", PR).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
            System.Threading.Thread.Sleep(3000);
            return RedirectToAction("IndexArchived");
        }

        public ActionResult IndexLeaveRequest()
        {
            HttpClient HTTPC = new HttpClient();
            HTTPC.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/rest/");
            HTTPC.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage HTTPRM = HTTPC.GetAsync("Request/Leave").Result;
            if (HTTPRM.IsSuccessStatusCode)
            {
                ViewBag.result = HTTPRM.Content.ReadAsAsync<IEnumerable<LeaveRequest>>().Result;
            }
            else
            {
                ViewBag.result = "Error !";
            }
            return View();
        }

        [HttpGet]
        public ActionResult CreateLeaveRequest()
        {
            HttpClient HTTPC = new HttpClient();
            HTTPC.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/rest/");
            HTTPC.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage HTTPRM = HTTPC.GetAsync("Leavetype").Result;
            if (HTTPRM.IsSuccessStatusCode)
            {
                ViewBag.result = HTTPRM.Content.ReadAsAsync<IEnumerable<LeaveType>>().Result;
                List<SelectListItem> Leavetypes = new List<SelectListItem>();
                foreach(var lt in ViewBag.result)
                {
                    Leavetypes.Add(new SelectListItem { Text = lt.name, Value = lt.idLeaveType + "" });
                }
                ViewBag.leavetypes = Leavetypes;
            }
            else
            {
                ViewBag.result = "Error !";
            }
            return View("CreateLeaveRequest");
        }

        [HttpPost]
        public ActionResult CreateLeaveRequest(LeaveRequest LR)
        {
            int id = int.Parse(Request.Form["leavetypes"]);
            LeaveType LT = new LeaveType
            {
                idLeaveType = id
            };
            LR.leaveType = LT;
            HttpClient HTTPC = new HttpClient();
            HTTPC.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/rest/");
            HTTPC.PostAsJsonAsync<LeaveRequest>("Request/Leave", LR).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
            System.Threading.Thread.Sleep(3000);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> UpdateLeaveRequest(int ID)
        {
            LeaveRequest LR = new LeaveRequest
            {
                idLeaveRequest = ID
            };
            HttpClient HTTPC = new HttpClient();
            HTTPC.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/rest/");
            HTTPC.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var R = await HTTPC.PostAsJsonAsync<LeaveRequest>("Request/Leave/findone", LR);
            ViewBag.request = R.Content.ReadAsAsync<LeaveRequest>().Result;
            HttpClient HTTPC0 = new HttpClient();
            HTTPC0.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/rest/");
            HTTPC0.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage HTTPRM = HTTPC0.GetAsync("Leavetype").Result;
            if (HTTPRM.IsSuccessStatusCode)
            {
                ViewBag.result = HTTPRM.Content.ReadAsAsync<IEnumerable<LeaveType>>().Result;
                List<SelectListItem> Leavetypes = new List<SelectListItem>();
                foreach (var lt in ViewBag.result)
                {
                    if (lt.idLeaveType == ViewBag.request.leaveType.idLeaveType)
                    {
                        Leavetypes.Add(new SelectListItem { Text = lt.name, Value = lt.idLeaveType + "", Selected=true });
                    }
                    else
                    {
                        Leavetypes.Add(new SelectListItem { Text = lt.name, Value = lt.idLeaveType + "" });
                    }
                }
                ViewBag.leavetypes = Leavetypes;
            }
            else
            {
                ViewBag.result = "Error !";
            }
            ViewBag.fromDate = ConvertDatetoDateTime(ViewBag.request.fromDate.ToString());
            ViewBag.toDate = ConvertDatetoDateTime(ViewBag.request.toDate.ToString());
            return View("UpdateLeaveRequest", ViewBag.request);
        }

        [HttpPost]
        public ActionResult UpdateLeaveRequest(LeaveRequest LR)
        {
            int id = int.Parse(Request.Form["leavetypes"]);
            LeaveType LT = new LeaveType
            {
                idLeaveType = id
            };
            LR.leaveType = LT;
            HttpClient HTTPC = new HttpClient();
            HTTPC.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/rest/");
            HTTPC.PutAsJsonAsync<LeaveRequest>("Request/Leave", LR).ContinueWith((task) => task.Result.EnsureSuccessStatusCode());
            System.Threading.Thread.Sleep(3000);
            return RedirectToAction("IndexLeaveRequest");
        }

        public ActionResult ArchiveLeaveRequest(int ID)
        {
            LeaveRequest LR = new LeaveRequest
            {
                idLeaveRequest = ID
            };
            HttpClient HTTPC = new HttpClient();
            HTTPC.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/rest/");
            HTTPC.PostAsJsonAsync<LeaveRequest>("Request/Leave/remove", LR).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
            System.Threading.Thread.Sleep(3000);
            return RedirectToAction("IndexLeaveRequest");
        }

        public ActionResult IndexArchivedLeaveRequest()
        {
            HttpClient HTTPC = new HttpClient();
            HTTPC.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/rest/");
            HTTPC.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage HTTPRM = HTTPC.GetAsync("Request/Leave/archive").Result;
            if (HTTPRM.IsSuccessStatusCode)
            {
                ViewBag.result = HTTPRM.Content.ReadAsAsync<IEnumerable<LeaveRequest>>().Result;
            }
            else
            {
                ViewBag.result = "Error !";
            }
            return View("IndexArchivedLeaveRequest");
        }

        public ActionResult UnarchiveLeaveRequest(int ID)
        {
            LeaveRequest LR = new LeaveRequest
            {
                idLeaveRequest = ID
            };
            HttpClient HTTPC = new HttpClient();
            HTTPC.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/rest/");
            HTTPC.PostAsJsonAsync<LeaveRequest>("Request/Leave/dearchive", LR).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
            System.Threading.Thread.Sleep(3000);
            return RedirectToAction("IndexArchivedLeaveRequest");
        }

        public string ConvertDate(string dotnetdate)
        {
            string subdate = dotnetdate.Substring(0, 10);
            string[] splitdate = subdate.Split('/');
            string date = splitdate[2] + "-" + splitdate[1] + "-" + splitdate[0];
            return date;
        }

        public string ConvertDatetoDateTime(string dotnetdate)
        {
            string date = ConvertDate(dotnetdate);
            string time = dotnetdate.Substring(11);
            string datetime = date + "T" + time;
            System.Diagnostics.Debug.WriteLine(datetime);
            return datetime;
        }
    }
}