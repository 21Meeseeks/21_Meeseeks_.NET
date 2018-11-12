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
    public class ClientCategoryController : Controller
    {
        // GET: ClientCategory
        public ActionResult Index()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress=new Uri("http://localhost:18080/21meeseeks-web/rest/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue ("application/json"));
            HttpResponseMessage response = client.GetAsync("client/category").Result;
       
                ViewBag.result = response.Content.ReadAsAsync<IEnumerable<ClientCategory>>().Result;
         
             
            
            return View();
        }

        // GET: ClientCategory/Details/5
        public ActionResult Details(int id)
        {
            return View();  
        }

        // GET: ClientCategory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientCategory/Create
        [HttpPost]
        public ActionResult Create(ClientCategory cc)
        {   
            HttpClient client = new HttpClient();
            //  var token=   Session["token"].ToString();
           
           // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            client.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/rest/");
   
                client.PostAsJsonAsync<ClientCategory>("client/category", cc).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
            System.Threading.Thread.Sleep(3000);

            return RedirectToAction("index");
        }

        // GET: ClientCategory/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ClientCategory/Edit/5
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

        // GET: ClientCategory/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ClientCategory/Delete/5
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
