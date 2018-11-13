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

        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }

        // GET: Request/Update
        [HttpGet]
        public ActionResult Update(int ID)
        {
            ProjectRequest PR = new ProjectRequest
            {
                idRequest = ID
            };
            HttpClient HTTPC = new HttpClient();
            HTTPC.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/rest/");
            HTTPC.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HTTPC.PostAsJsonAsync<ProjectRequest>("Request/findone", PR).ContinueWith(httpResponseMessage =>
            {
                if (httpResponseMessage.Result.IsSuccessStatusCode)
                {
                    ViewBag.request = httpResponseMessage.Result.Content.ReadAsAsync<ProjectRequest>().Result;
                }
            });
            return View("Update");
        }

        [HttpPost]
        public ActionResult Create(ProjectRequest PR)
        {
            HttpClient HTTPC = new HttpClient();
            HTTPC.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/rest/");
            HTTPC.PostAsJsonAsync<ProjectRequest>("Request", PR).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode()).ConfigureAwait(true);
            System.Threading.Thread.Sleep(3000);
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

        [HttpPost]
        public ActionResult Update(ProjectRequest PR)
        {
            HttpClient HTTPC = new HttpClient();
            HTTPC.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/rest/");
            HTTPC.PutAsJsonAsync<ProjectRequest>("Request", PR).ContinueWith((task) => task.Result.EnsureSuccessStatusCode());
            return RedirectToAction("Index");
        }
    }
}